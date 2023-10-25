using AutoMapper;
using StoreHouse.Api.Model.DTO.ProductListDTO;
using StoreHouse.Api.Model.DTO.StorageDTO;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;

namespace StoreHouse.Api.Services;

public class StorageService : IStorageService
{
    private readonly ISemiProductService _semiProductService;
    private readonly IDishService _dishService;
    private readonly IWriteOffService _writeOffService;
    private readonly IWriteOffCauseService _writeOffCauseService;
    private readonly ISupplyService _supplyService;
    private readonly IProductService _productService;
    private readonly IIngredientService _ingredientService;
    private readonly ISupplierService _supplierService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public StorageService(IWriteOffCauseService writeOffCauseService, IDishService dishService, ISemiProductService semiProductService, IUserService userService, ISupplierService supplierService, IWriteOffService writeOffService, IProductService productService, IIngredientService ingredientService, ISupplyService supplyService, IMapper mapper)
    {
        _writeOffCauseService = writeOffCauseService;
        _dishService = dishService;
        _semiProductService = semiProductService;
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
            var allProductsResponse = _mapper.Map<List<StorageRemainResponse>>(products.ProductList);
            if (allProductsResponse == null)
                return (false, "Mapping failed, object is null", new List<StorageRemainResponse>());
            var allIngredientsResponse = _mapper.Map<List<StorageRemainResponse>>(ingredients.IngredientList);
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
            var allSuppliesResponse = _mapper.Map<List<StorageSupplyResponse>>(supplies.SupplyList);
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
            else
            {
                return (false, "There is no Ingredient or Product with this name", -1);
            }
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
            else
            {
                return (false, "There is no Ingredient or Product with this name");
            }
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

    public async Task<(bool IsSuccess, string ErrorMessage, List<StorageWriteOffResponse> AllWriteOffs)> GetAllWriteOffsAsync()
    {
        //Get all WriteOffs
        var writeOffs = await _writeOffService.GetAllWriteOffsAsync();
        if (!writeOffs.IsSuccess)
            return (false, writeOffs.ErrorMessage, new List<StorageWriteOffResponse>());
        
        //Mapping WriteOff to StorageWriteOffResponse
        var writeOffMap = _mapper.Map<List<StorageWriteOffResponse>>(writeOffs.WriteOffList);
        if (writeOffMap == null)
            return (false, "Mapping failed, object is null", new List<StorageWriteOffResponse>());
        
        //Change required fields
        foreach (var writeOffResult in writeOffMap)
        {
            foreach (var writeOff in writeOffs.WriteOffList.Where(wr => wr.Id == writeOffResult.Id))
            {
                writeOffResult.ProductList = _mapper.Map<List<WriteOffProductListResponse>>(writeOff.ProductLists);
            }
        }

        return (true, string.Empty, writeOffMap);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateWriteOffAsync(StorageWriteOffRequest updatedWriteOff)
    {
        //Mapping StorageWriteOffRequest to WriteOff and WriteOffProductListRequest to ProductList
        var writeOffMap = _mapper.Map<WriteOff>(updatedWriteOff);
        if(writeOffMap == null)
            return (false, "Mapping failed, object is null", -1);
        var productListMap = _mapper.Map<List<ProductList>>(updatedWriteOff.ProductList);
        if(productListMap == null)
            return (false, "Mapping failed, object is null", -1);
        
        //Change required fields
        foreach (var product in productListMap)
        {
            var primeCostIngredientResult = await _ingredientService.GetPrimeCostByName(product.Name);
            var primeCostProductResult = await _productService.GetPrimeCostByName(product.Name); 
            var primeCostSemiProductResult = await _semiProductService.GetPrimeCostByName(product.Name); 
            var dishProductListResult = await _dishService.GetProductListByName(product.Name); 
            if (primeCostIngredientResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostIngredientResult.PrimeCost;
            }
            else if (primeCostProductResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostProductResult.PrimeCost;
            }
            else if (primeCostSemiProductResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostSemiProductResult.PrimeCost;
            }
            else if (dishProductListResult.IsSuccess)
            {
                decimal sum = 0;
                foreach (var prod in dishProductListResult.ProductList)
                {
                    sum += prod.PrimeCost;
                }
                
                product.Price = (decimal)product.Count * sum;
            }

            return (false, "There is no Ingredient or Product with this name", -1);
        }
        
        //Get Supplier Id by Name
        var writeOffCauseIdResult = await _writeOffCauseService.GetIdByName(updatedWriteOff.Cause);
        if (!writeOffCauseIdResult.IsSuccess)
            return (false, writeOffCauseIdResult.ErrorMessage, -1);

        writeOffMap.CauseId = writeOffCauseIdResult.Id;
        
        //Call the DAL update service
        var updateWriteOff = await _writeOffService.UpdateWriteOffAsync(writeOffMap);
        if (!updateWriteOff.IsSuccess)
            return (false, updateWriteOff.ErrorMessage, -1);

        return (true, string.Empty, updatedWriteOff.Id);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddWriteOffAsync(StorageWriteOffRequest writeOff, string userLogin)
    {
        //Mapping StorageWriteOffRequest to WriteOff and WriteOffProductListRequest to ProductList
        var writeOffMap = _mapper.Map<WriteOff>(writeOff);
        if(writeOffMap == null)
            return (false, "Mapping failed, object is null");
        var productListMap = _mapper.Map<List<ProductList>>(writeOff.ProductList);
        if(productListMap == null)
            return (false, "Mapping failed, object is null");
        
        //Change required fields
        foreach (var product in productListMap)
        {
            var primeCostIngredientResult = await _ingredientService.GetPrimeCostByName(product.Name);
            var primeCostProductResult = await _productService.GetPrimeCostByName(product.Name); 
            var primeCostSemiProductResult = await _semiProductService.GetPrimeCostByName(product.Name); 
            var dishProductListResult = await _dishService.GetProductListByName(product.Name); 
            if (primeCostIngredientResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostIngredientResult.PrimeCost;
            }
            else if (primeCostProductResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostProductResult.PrimeCost;
            }
            else if (primeCostSemiProductResult.IsSuccess)
            {
                product.Price = (decimal)product.Count * primeCostSemiProductResult.PrimeCost;
            }
            else if (dishProductListResult.IsSuccess)
            {
                decimal sum = 0;
                foreach (var prod in dishProductListResult.ProductList)
                {
                    sum += prod.PrimeCost;
                }
                
                product.Price = (decimal)product.Count * sum;
            }
            else
            {
                return (false, "There is no Ingredient or Product with this name");
            }
        }
        
        //Get Write Off Cause Id by Name
        var writeOffCauseIdResult = await _writeOffCauseService.GetIdByName(writeOff.Cause);
        if (!writeOffCauseIdResult.IsSuccess)
            return (false, writeOffCauseIdResult.ErrorMessage);

        //Get User by Login
        var user = await _userService.GetUserByLogin(userLogin);
        if (!user.IsSuccess)
            return (false, user.ErrorMessage);
        //Change required properties
        writeOffMap.UserId = user.User.Id;
        writeOffMap.UserName = user.User.FullName;
        writeOffMap.CauseId = writeOffCauseIdResult.Id;
        writeOffMap.ProductLists = productListMap;
        //Call the DAL update service
        var updateWriteOff = await _writeOffService.CreateWriteOffAsync(writeOffMap);
        if (!updateWriteOff.IsSuccess)
            return (false, updateWriteOff.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffAsync(int writeOffId)
    {
        var deletedWriteOff = await _writeOffService.DeleteWriteOffAsync(writeOffId);
        if (!deletedWriteOff.IsSuccess)
            return (false, deletedWriteOff.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<StorageWriteOffCauseResponse> AllWriteOffCauses)> GetAllWriteOffCausesAsync()
    {
        //Get Suppliers
        var writeOffCauses = await _writeOffCauseService.GetAllWriteOffCausesAsync();
        if (!writeOffCauses.IsSuccess)
            return (false, writeOffCauses.ErrorMessage, new List<StorageWriteOffCauseResponse>());

        //Map Product Categories
        var writeOffCausesMap = _mapper.Map<List<StorageWriteOffCauseResponse>>(writeOffCauses.WriteOffCauseList);
        if (writeOffCausesMap == null)
            return (false, "Mapping failed, object is null", new List<StorageWriteOffCauseResponse>());

        foreach (var writeOffCauseResponse in writeOffCausesMap)
        {
            foreach (var writeOff in writeOffCauses.WriteOffCauseList.Where(wr => wr.Id == writeOffCauseResponse.Id))
            {
                decimal writeOffSum = 0;
                foreach (var wrOff in writeOff.WriteOffs)
                {
                    var sum = GetSum(wrOff.ProductLists);
                    if (!sum.IsSuccess)
                        return (false, sum.ErrorMessage, new List<StorageWriteOffCauseResponse>());

                    writeOffSum += sum.Sum;
                }
                writeOffCauseResponse.WriteOffCount = writeOff.WriteOffs.Count;
                writeOffCauseResponse.WriteOffSum = writeOffSum;
            }
        }

        return (true, string.Empty, writeOffCausesMap);
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage)> AddWriteOffCauseAsync(StorageWriteOffCauseRequest writeOffCause)
    {
        //Map Product Category
        var writeOffCauseMap = _mapper.Map<WriteOffCause>(writeOffCause);
        if (writeOffCauseMap == null)
            return (false, "Mapping failed, object is null");
        
        //Call the DAL update service
        var addWriteOffCause = await _writeOffCauseService.CreateWriteOffCauseAsync(writeOffCauseMap);
        if (!addWriteOffCause.IsSuccess)
            return (false, addWriteOffCause.ErrorMessage);

        return (true, string.Empty);
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffCauseAsync(int writeOffCauseId)
    {
        var deletedWriteOffCause = await _writeOffCauseService.DeleteWriteOffCauseAsync(writeOffCauseId);
        if (!deletedWriteOffCause.IsSuccess)
            return (false, deletedWriteOffCause.ErrorMessage);

        return (true, string.Empty);
    }
    public async Task<(bool IsSuccess, string ErrorMessage, List<StorageSupplierResponse> AllSuppliers)> GetAllSuppliersAsync()
    {
        //Get Suppliers
        var suppliers = await _supplierService.GetAllSuppliersAsync();
        if (!suppliers.IsSuccess)
            return (false, suppliers.ErrorMessage, new List<StorageSupplierResponse>());

        //Map Product Categories
        var suppliersMap = _mapper.Map<List<StorageSupplierResponse>>(suppliers.SupplierList);
        if (suppliersMap == null)
            return (false, "Mapping failed, object is null", new List<StorageSupplierResponse>());

        foreach (var supplierResponse in suppliersMap)
        {
            foreach (var supplier in suppliers.SupplierList.Where(supply => supply.Id == supplierResponse.Id))
            {
                decimal suppliesSum = 0;
                foreach (var suply in supplier.Supplies)
                {
                    var sum = GetSum(suply.ProductLists);
                    if (!sum.IsSuccess)
                        return (false, sum.ErrorMessage, new List<StorageSupplierResponse>());

                    suppliesSum += sum.Sum;
                }
                supplierResponse.SuppliesCount = supplier.Supplies.Count;
                supplierResponse.SupplySum = suppliesSum;
            }
        }

        return (true, string.Empty, suppliersMap);
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage)> AddSupplierAsync(StorageSupplierRequest supplier)
    {
        //Map Product Category
        var supplierMap = _mapper.Map<Supplier>(supplier);
        if (supplierMap == null)
            return (false, "Mapping failed, object is null");
        
        //Call the DAL update service
        var addSupplier = await _supplierService.CreateSupplierAsync(supplierMap);
        if (!addSupplier.IsSuccess)
            return (false, addSupplier.ErrorMessage);

        return (true, string.Empty);
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplierAsync(int supplierId)
    {
        var deletedSupplier = await _supplierService.DeleteSupplierAsync(supplierId);
        if (!deletedSupplier.IsSuccess)
            return (false, deletedSupplier.ErrorMessage);

        return (true, string.Empty);
    }
    
    //Static method to Get Sum of products in ProductLists
    private static (bool IsSuccess, string ErrorMessage, decimal Sum) GetSum(List<ProductList> productLists)
    {
        try
        {
            decimal sum = 0;

            foreach (var product in productLists)
            {
                sum += product.Price;
            }

            return (true, string.Empty, sum);
        }
        catch (Exception e)
        {
            return (false, e.Message, 0);
        }
    }
    
    
}