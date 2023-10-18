﻿using AutoMapper;
using StoreHouse.Api.Model.DTO.ProductListDTO;
using StoreHouse.Api.Model.DTO.StorageDTO;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;

namespace StoreHouse.Api.Services;

public class StorageService : IStorageService
{
    private readonly IWriteOffService _writeOffService;
    private readonly ISupplyService _supplyService;
    private readonly IProductService _productService;
    private readonly IIngredientService _ingredientService;
    private readonly ISupplierService _supplierService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public StorageService(IUserService userService, ISupplierService supplierService, IWriteOffService writeOffService, IProductService productService, IIngredientService ingredientService, ISupplyService supplyService, IMapper mapper)
    {
        _userService = userService;
        _supplierService = supplierService;
        _supplyService = supplyService;
        _productService = productService;
        _writeOffService = writeOffService;
        _ingredientService = ingredientService;
        _mapper = mapper;
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage, List<StorageRemainResponse> AllRemains)> GetAllRemainsAsync()
    {
        try
        {
            //Get all Products
            var products = await _productService.GetAllProductsAsync();
            if (!products.IsSuccess)
                return (false, products.ErrorMessage, new List<StorageRemainResponse>());
            //Get all Ingredients
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            if (!ingredients.IsSuccess)
                return (false, ingredients.ErrorMessage, new List<StorageRemainResponse>());

            //Mapping Products and Ingredients
            var allProductsResponse = _mapper.Map<List<StorageRemainResponse>>(products);
            if (allProductsResponse == null)
                return (false, "Mapping failed, object is null", new List<StorageRemainResponse>());
            var allIngredientsResponse = _mapper.Map<List<StorageRemainResponse>>(ingredients);
            if (allIngredientsResponse == null)
                return (false, "Mapping failed, object is null", new List<StorageRemainResponse>());

            //Change required fields
            foreach (var product in allProductsResponse)
            {
                product.Type = "Товар";
                product.Sum = product.PrimeCost * (decimal)product.Remains;
            }

            foreach (var ingredient in allIngredientsResponse)
            {
                ingredient.Type = "Інгредієнт";
                ingredient.Sum = ingredient.PrimeCost * (decimal)ingredient.Remains;
            }

            var remainsResponse = new List<StorageRemainResponse>();
            remainsResponse.AddRange(allProductsResponse);
            remainsResponse.AddRange(allIngredientsResponse);

            return (true, string.Empty, remainsResponse);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<StorageRemainResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<StorageSupplyResponse> AllSupplies)> GetAllSuppliesAsync()
    {
        try
        {
            //Get All Supplies
            var supplies = await _supplyService.GetAllSuppliesAsync();
            if (!supplies.IsSuccess)
                return (false, supplies.ErrorMessage, new List<StorageSupplyResponse>());

            //Mapping Supplies and ProductList
            var allSuppliesResponse = _mapper.Map<List<StorageSupplyResponse>>(supplies);
            if (allSuppliesResponse == null)
                return (false, "Mapping failed, object is null", new List<StorageSupplyResponse>());

            //Change required fields
            foreach (var supplyResponse in allSuppliesResponse)
            {
                foreach (var supply in supplies.SupplyList.Where(supply => supply.Id == supplyResponse.Id))
                {
                    supplyResponse.ProductList = _mapper.Map<List<SupplyProductListResponse>>(supply.ProductLists);
                }
            }

            return (true, string.Empty, allSuppliesResponse);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<StorageSupplyResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateSupplyAsync(StorageSupplyRequest updatedSupply)
    {
        //Mapping StorageSupplyRequest to Supply and SupplyProductListRequest to ProductList
        var supplyMap = _mapper.Map<Supply>(updatedSupply);
        if(supplyMap == null)
            return (false, "Mapping failed, object is null", -1);
        var productListMap = _mapper.Map<List<ProductList>>(updatedSupply.ProductList);
        if(productListMap == null)
            return (false, "Mapping failed, object is null", -1);
        
        //Change required fields
        foreach (var product in productListMap)
        {
            var primeCostIngredientResult = await _ingredientService.GetPrimeCostByName(product.Name);
            var primeCostProductResult = await _productService.GetPrimeCostByName(product.Name); 
            if (primeCostIngredientResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostIngredientResult.PrimeCost;
            }
            else if (primeCostProductResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostProductResult.PrimeCost;
            }

            return (false, "There is no Ingredient or Product with this name", -1);
        }

        //Get Supplier Id by Name
        var supplierIdResult = await _supplierService.GetIdByName(updatedSupply.SupplierName);
        if (!supplierIdResult.IsSuccess)
            return (false, supplierIdResult.ErrorMessage, -1);
        
        //Get ProductList Sum
        var sum = GetSum(productListMap);
        if (!sum.IsSuccess)
            return (false, sum.ErrorMessage, -1);

        //Change required properties
        supplyMap.Sum = sum.Sum;
        supplyMap.SupplierId = supplierIdResult.Id;
        supplyMap.ProductLists = productListMap;
        
        //Call the DAL update service
        var updateSupply = await _supplyService.UpdateSupplyAsync(supplyMap);
        if (!updateSupply.IsSuccess)
            return (false, updateSupply.ErrorMessage, -1);

        return (true, string.Empty, updatedSupply.Id);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddSupplyAsync(StorageSupplyRequest supply, string userLogin)
    {
        //Mapping StorageSupplyRequest to Supply and SupplyProductListRequest to ProductList
        var supplyMap = _mapper.Map<Supply>(supply);
        if(supplyMap == null)
            return (false, "Mapping failed, object is null");
        var productListMap = _mapper.Map<List<ProductList>>(supply.ProductList);
        if(productListMap == null)
            return (false, "Mapping failed, object is null");
        
        //Change required fields
        foreach (var product in productListMap)
        {
            var primeCostIngredientResult = await _ingredientService.GetPrimeCostByName(product.Name);
            var primeCostProductResult = await _productService.GetPrimeCostByName(product.Name); 
            if (primeCostIngredientResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostIngredientResult.PrimeCost;
            }
            else if (primeCostProductResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostProductResult.PrimeCost;
            }

            return (false, "There is no Ingredient or Product with this name");
        }

        //Get Supplier Id by Name
        var supplierIdResult = await _supplierService.GetIdByName(supply.SupplierName);
        if (!supplierIdResult.IsSuccess)
            return (false, supplierIdResult.ErrorMessage);
        
        //Get ProductList Sum
        var sum = GetSum(productListMap);
        if (!sum.IsSuccess)
            return (false, sum.ErrorMessage);
        
        //Get User by Login
        var user = await _userService.GetUserByLogin(userLogin);
        if (!user.IsSuccess)
            return (false, user.ErrorMessage);

        //Change required properties
        supplyMap.UserId = user.User.Id;
        supplyMap.UserName = user.User.FullName;
        supplyMap.Sum = sum.Sum;
        supplyMap.SupplierId = supplierIdResult.Id;
        supplyMap.ProductLists = productListMap;
        
        //Call the DAL update service
        var addSupply = await _supplyService.CreateSupplyAsync(supplyMap);
        if (!addSupply.IsSuccess)
            return (false, addSupply.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplyAsync(int supplyId)
    {
        var deletedSupply = await _supplyService.DeleteSupplyAsync(supplyId);
        if (!deletedSupply.IsSuccess)
            return (false, deletedSupply.ErrorMessage);

        return (true, string.Empty);
    }

    public Task<(bool IsSuccess, string ErrorMessage, List<StorageWriteOffResponse> AllriteOffs)> GetAllWriteOffsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateWriteOffAsync(StorageWriteOffRequest updatedWriteOff)
    {
        throw new NotImplementedException();
    }

    public Task<(bool IsSuccess, string ErrorMessage)> AddWriteOffAsync(StorageWriteOffRequest supply)
    {
        throw new NotImplementedException();
    }

    public Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffAsync(int supplyId)
    {
        throw new NotImplementedException();
    }
    
    
    //Static method to Get Sum of products in ProductLists
    private static (bool IsSuccess, string ErrorMessage, decimal Sum) GetSum(List<ProductList> productLists)
    {
        try
        {
            decimal sum = 0;

            foreach (var product in productLists)
            {
                sum += product.Price * (decimal)product.Count;
            }

            return (true, string.Empty, sum);
        }
        catch (Exception e)
        {
            return (false, e.Message, 0);
        }
    }
}