using AutoMapper;
using StoreHouse.Api.Model.DTO.MenuDTO;
using StoreHouse.Api.Model.DTO.ProductListDTO;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;

namespace StoreHouse.Api.Services;

public class MenuService : IMenuService
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _productCategoryService;
    private readonly IDishService _dishService;
    private readonly IMapper _mapper;

    public MenuService(IDishService dishService, IProductCategoryService productCategoryService, IMapper mapper, IProductService productService)
    {
        _dishService = dishService;
        _productCategoryService = productCategoryService;
        _mapper = mapper;
        _productService = productService;
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuProductResponse> AllProducts)> GetAllProductsAsync()
    {
        //Get All Products
        var products = await _productService.GetAllProductsAsync();
        if (!products.IsSuccess)
            return (false, products.ErrorMessage, new List<MenuProductResponse>());

        //Mapping Products
        var productMap = _mapper.Map<List<MenuProductResponse>>(products);
        if (productMap == null)
            return (false, "Mapping failed, object is null", new List<MenuProductResponse>());
        
        return (true, string.Empty, productMap);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateProductAsync(MenuProductRequest updatedProduct)
    {
        //Mapping Products
        var productMap = _mapper.Map<Product>(updatedProduct);
        if (productMap == null)
            return (false, "Mapping failed, object is null", updatedProduct.Id);
        
        //Change Required Data
        var categoryId = await _productCategoryService.GetCategoryIdByNameAsync(updatedProduct.CategoryName);
        if (!categoryId.IsSuccess)
            return (false, categoryId.ErrorMessage, -1);

        productMap.CategoryId = categoryId.CategoryId;
        
        //Call the DAL update service
        var updateProduct = await _productService.UpdateProductAsync(productMap);
        if (!updateProduct.IsSuccess)
            return (false, updateProduct.ErrorMessage, -1);

        return (true, string.Empty, updatedProduct.Id);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddProductAsync(MenuProductRequest product)
    {
        //Mapping Products
        var productMap = _mapper.Map<Product>(product);
        if (productMap == null)
            return (false, "Mapping failed, object is null");
        
        //Change Required Data
        var categoryId = await _productCategoryService.GetCategoryIdByNameAsync(product.CategoryName);
        if (!categoryId.IsSuccess)
            return (false, categoryId.ErrorMessage);

        productMap.CategoryId = categoryId.CategoryId;
        
        //Call the DAL update service
        var addProduct = await _productService.CreateProductAsync(productMap);
        if (!addProduct.IsSuccess)
            return (false, addProduct.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(int productId)
    {
        var deletedProduct = await _productService.DeleteProductAsync(productId);
        if (!deletedProduct.IsSuccess)
            return (false, deletedProduct.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuDishResponse> AllDishes)> GetAllDishesAsync()
    {
        //Get All Dishes
        var dishes = await _dishService.GetAllDishesAsync();
        if (!dishes.IsSuccess)
            return (false, dishes.ErrorMessage, new List<MenuDishResponse>());

        //Mapping Dishes and Product List
        var dishMap = _mapper.Map<List<MenuDishResponse>>(dishes);
        if (dishMap == null)
            return (false, "Mapping failed, object is null", new List<MenuDishResponse>());
        foreach (var dishResponse in dishMap)
        {
            foreach (var dish in dishes.DishList.Where(d => d.Id == dishResponse.Id))
            {
                var productListMap = _mapper.Map<List<MenuProductListResponse>>(dish.ProductLists);
                if (productListMap == null)
                    return (false, "Mapping failed, object is null", new List<MenuDishResponse>());
                var weight = GetWeight(dish.ProductLists);
                if (!weight.IsSuccess)
                    return (false, weight.ErrorMessage, new List<MenuDishResponse>());
                var primeCost = GetPrimeCost(dish.ProductLists);
                if (!primeCost.IsSuccess)
                    return (false, primeCost.ErrorMessage, new List<MenuDishResponse>());
                
                dishResponse.ProductList = productListMap;
                dishResponse.Weight = weight.Weight;
                dishResponse.PrimeCost = primeCost.PrimeCost;
            }
        }
        
        return (true, string.Empty, dishMap);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateDishAsync(MenuDishRequest updatedDish)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddDishAsync(MenuDishRequest dish)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteDishAsync(int dishId)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuSemiProductResponse> AllSemiProducts)> GetAllSemiProductsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateSemiProductAsync(MenuSemiProductRequest updatedSemiProduct)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddSemiProductAsync(MenuSemiProductRequest semiProduct)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteSemiProductAsync(int semiProductId)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuIngredientResponse> AllIngredients)> GetAllIngredientsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateIngredientAsync(MenuIngredientAddRequest updatedIngredient)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddIngredientAsync(MenuIngredientUpdateRequest ingredient)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientAsync(int ingredientId)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuProductCategoryResponse> AllProductCategories)> GetAllProductCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddProductCategoryAsync(MenuProductCategoryRequest productCategory)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductCategoryAsync(int productCategoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuIngredientCategoryResponse> AllIngredientCategories)> GetAllIngredientCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddIngredientCategoryAsync(MenuIngredientCategoryRequest ingredientCategory)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientCategoryAsync(int ingredientCategoryId)
    {
        throw new NotImplementedException();
    }

    private static (bool IsSuccess, string ErrorMessage, double Weight) GetWeight(List<ProductList> productList)
    {
        double weight = 0;
        foreach (var product in productList)
        {
            weight += product.Count;
        }

        return (true, string.Empty, weight);
    }
    
    private static (bool IsSuccess, string ErrorMessage, decimal PrimeCost) GetPrimeCost(List<ProductList> productList)
    {
        decimal primeCost = 0;
        foreach (var product in productList)
        {
            primeCost += product.PrimeCost;
        }

        return (true, string.Empty, primeCost);
    }
}