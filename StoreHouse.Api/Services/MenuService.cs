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
    private readonly IIngredientCategoryService _ingredientCategoryService;
    private readonly IIngredientService _ingredientService;
    private readonly ISemiProductService _semiProductService;
    private readonly IProductCategoryService _productCategoryService;
    private readonly IDishService _dishService;
    private readonly IMapper _mapper;

    public MenuService(IIngredientCategoryService ingredientCategoryService, ISemiProductService semiProductService, IIngredientService ingredientService, IDishService dishService, IProductCategoryService productCategoryService, IMapper mapper, IProductService productService)
    {
        _ingredientCategoryService = ingredientCategoryService;
        _ingredientService = ingredientService;
        _semiProductService = semiProductService;
        _dishService = dishService;
        _productCategoryService = productCategoryService;
        _mapper = mapper;
        _productService = productService;
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuProductResponse> AllProducts)> GetAllProductsAsync()
    {
        try
        {
            //Get All Products
            var products = await _productService.GetAllProductsAsync();
            if (!products.IsSuccess)
                return (false, products.ErrorMessage, new List<MenuProductResponse>());

            //Mapping Products
            var productMap = _mapper.Map<List<MenuProductResponse>>(products.ProductList);
            if (productMap == null)
                return (false, "Mapping failed, object is null", new List<MenuProductResponse>());

            return (true, string.Empty, productMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<MenuProductResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateProductAsync(MenuProductRequest updatedProduct)
    {
        try
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
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddProductAsync(MenuProductRequest product)
    {
        try
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
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(int productId)
    {
        try
        {
            var deletedProduct = await _productService.DeleteProductAsync(productId);
            if (!deletedProduct.IsSuccess)
                return (false, deletedProduct.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuDishResponse> AllDishes)> GetAllDishesAsync()
    {
        try
        {
            //Get All Dishes
            var dishes = await _dishService.GetAllDishesAsync();
            if (!dishes.IsSuccess)
                return (false, dishes.ErrorMessage, new List<MenuDishResponse>());

            //Mapping Dishes and Product List
            var dishMap = _mapper.Map<List<MenuDishResponse>>(dishes.DishList);
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
        catch (Exception e)
        {
            return (false, e.Message, new List<MenuDishResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateDishAsync(MenuDishRequest updatedDish)
    {
        try
        {
            //Mapping Products
            var dishMap = _mapper.Map<Dish>(updatedDish);
            if (dishMap == null)
                return (false, "Mapping failed, object is null", updatedDish.Id);
            var productListMap = _mapper.Map<List<ProductList>>(updatedDish.ProductList);
            if (productListMap == null)
                return (false, "Mapping failed, object is null", updatedDish.Id);

            //Change Required Data
            var categoryId = await _productCategoryService.GetCategoryIdByNameAsync(updatedDish.CategoryName);
            if (!categoryId.IsSuccess)
                return (false, categoryId.ErrorMessage, -1);

            foreach (var product in productListMap)
            {
                var primeCostIngredientResult = await _ingredientService.GetPrimeCostByName(product.Name);
                var primeCostProductResult = await _productService.GetPrimeCostByName(product.Name);
                var primeCostSemiProductResult = await _semiProductService.GetPrimeCostByName(product.Name);
                var dishProductListResult = await _dishService.GetProductListByName(product.Name);
                if (primeCostIngredientResult.IsSuccess)
                {
                    product.PrimeCost = primeCostIngredientResult.PrimeCost;
                }
                else if (primeCostProductResult.IsSuccess)
                {
                    product.PrimeCost = primeCostProductResult.PrimeCost;
                }
                else if (primeCostSemiProductResult.IsSuccess)
                {
                    product.PrimeCost = primeCostSemiProductResult.PrimeCost;
                }
                else if (dishProductListResult.IsSuccess)
                {
                    decimal sum = 0;
                    foreach (var prod in dishProductListResult.ProductList)
                    {
                        sum += prod.PrimeCost;
                    }

                    product.PrimeCost = sum;
                }

                return (false, "There is no Ingredient or Product with this name", -1);
            }

            dishMap.ProductLists = productListMap;
            dishMap.CategoryId = categoryId.CategoryId;

            //Call the DAL update service
            var updateDish = await _dishService.UpdateDishAsync(dishMap);
            if (!updateDish.IsSuccess)
                return (false, updateDish.ErrorMessage, -1);

            return (true, string.Empty, updatedDish.Id);
        }
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddDishAsync(MenuDishRequest dish)
    {
        try
        {
            //Mapping Products
            var dishMap = _mapper.Map<Dish>(dish);
            if (dishMap == null)
                return (false, "Mapping failed, object is null");
            var productListMap = _mapper.Map<List<ProductList>>(dish.ProductList);
            if (productListMap == null)
                return (false, "Mapping failed, object is null");

            //Change Required Data
            var categoryId = await _productCategoryService.GetCategoryIdByNameAsync(dish.CategoryName);
            if (!categoryId.IsSuccess)
                return (false, categoryId.ErrorMessage);

            foreach (var product in productListMap)
            {
                var primeCostIngredientResult = await _ingredientService.GetPrimeCostByName(product.Name);
                var primeCostProductResult = await _productService.GetPrimeCostByName(product.Name);
                var primeCostSemiProductResult = await _semiProductService.GetPrimeCostByName(product.Name);
                var dishProductListResult = await _dishService.GetProductListByName(product.Name);
                if (primeCostIngredientResult.IsSuccess)
                {
                    product.PrimeCost = primeCostIngredientResult.PrimeCost;
                }
                else if (primeCostProductResult.IsSuccess)
                {
                    product.PrimeCost = primeCostProductResult.PrimeCost;
                }
                else if (primeCostSemiProductResult.IsSuccess)
                {
                    product.PrimeCost = primeCostSemiProductResult.PrimeCost;
                }
                else if (dishProductListResult.IsSuccess)
                {
                    decimal sum = 0;
                    foreach (var prod in dishProductListResult.ProductList)
                    {
                        sum += prod.PrimeCost;
                    }

                    product.PrimeCost = sum;
                }
                else
                {
                    return (false, "There is no Ingredient or Product with this name");
                }
            }

            dishMap.ProductLists = productListMap;
            dishMap.CategoryId = categoryId.CategoryId;

            //Call the DAL update service
            var addDish = await _dishService.CreateDishAsync(dishMap);
            if (!addDish.IsSuccess)
                return (false, addDish.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteDishAsync(int dishId)
    {
        try
        {
            var deletedDish = await _dishService.DeleteDishAsync(dishId);
            if (!deletedDish.IsSuccess)
                return (false, deletedDish.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuSemiProductResponse> AllSemiProducts)> GetAllSemiProductsAsync()
    {
        try
        {
            //Get All Dishes
            var semiProducts = await _semiProductService.GetAllSemiProductsAsync();
            if (!semiProducts.IsSuccess)
                return (false, semiProducts.ErrorMessage, new List<MenuSemiProductResponse>());

            //Mapping Dishes and Product List
            var semiProductsMap = _mapper.Map<List<MenuSemiProductResponse>>(semiProducts.SemiProductList);
            if (semiProductsMap == null)
                return (false, "Mapping failed, object is null", new List<MenuSemiProductResponse>());
            foreach (var semiProductsResponse in semiProductsMap)
            {
                foreach (var semiProduct in semiProducts.SemiProductList.Where(d => d.Id == semiProductsResponse.Id))
                {
                    var productListMap = _mapper.Map<List<MenuProductListResponse>>(semiProduct.ProductLists);
                    if (productListMap == null)
                        return (false, "Mapping failed, object is null", new List<MenuSemiProductResponse>());

                    semiProductsResponse.ProductList = productListMap;
                }
            }

            return (true, string.Empty, semiProductsMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<MenuSemiProductResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateSemiProductAsync(MenuSemiProductRequest updatedSemiProduct)
    {
        try
        {
            //Mapping Products
            var semiProductMap = _mapper.Map<SemiProduct>(updatedSemiProduct);
            if (semiProductMap == null)
                return (false, "Mapping failed, object is null", updatedSemiProduct.Id);
            var productListMap = _mapper.Map<List<ProductList>>(updatedSemiProduct.ProductList);
            if (productListMap == null)
                return (false, "Mapping failed, object is null", updatedSemiProduct.Id);

            //Change Required Data
            foreach (var product in productListMap)
            {
                var primeCostIngredientResult = await _ingredientService.GetPrimeCostByName(product.Name);
                var primeCostProductResult = await _productService.GetPrimeCostByName(product.Name);
                if (primeCostIngredientResult.IsSuccess)
                {
                    product.PrimeCost = (decimal)product.Count * primeCostIngredientResult.PrimeCost;
                    semiProductMap.Output += product.Count;
                }
                else if (primeCostProductResult.IsSuccess)
                {
                    product.PrimeCost = (decimal)product.Count * primeCostProductResult.PrimeCost;
                    semiProductMap.Output += product.Count;
                }
                else
                {
                    return (false, "There is no Ingredient or Product with this name", -1);
                }
            }

            if (semiProductMap.Output < 1 || semiProductMap.Output > 1.0009)
                return (false, "semi-product should be made up per 1 kg of finished product", -1);
            semiProductMap.ProductLists = productListMap;

            //Call the DAL update service
            var updateSemiProduct = await _semiProductService.UpdateSemiProductAsync(semiProductMap);
            if (!updateSemiProduct.IsSuccess)
                return (false, updateSemiProduct.ErrorMessage, -1);

            return (true, string.Empty, updatedSemiProduct.Id);
        }
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddSemiProductAsync(MenuSemiProductRequest semiProduct)
    {
        try
        {
            //Mapping Products
            var semiProductMap = _mapper.Map<SemiProduct>(semiProduct);
            if (semiProductMap == null)
                return (false, "Mapping failed, object is null");
            var productListMap = _mapper.Map<List<ProductList>>(semiProduct.ProductList);
            if (productListMap == null)
                return (false, "Mapping failed, object is null");

            //Change Required Data
            foreach (var product in productListMap)
            {
                var primeCostIngredientResult = await _ingredientService.GetPrimeCostByName(product.Name);
                var primeCostProductResult = await _productService.GetPrimeCostByName(product.Name);
                if (primeCostIngredientResult.IsSuccess)
                {
                    product.PrimeCost = (decimal)product.Count * primeCostIngredientResult.PrimeCost;
                    semiProductMap.PrimeCost += product.PrimeCost;
                    semiProductMap.Output += product.Count;
                }
                else if (primeCostProductResult.IsSuccess)
                {
                    product.PrimeCost = (decimal)product.Count * primeCostProductResult.PrimeCost;
                    semiProductMap.PrimeCost += product.PrimeCost;
                    semiProductMap.Output += product.Count;
                }
                else
                {
                    return (false, "There is no Ingredient or Product with this name");
                }
            }

            if (semiProductMap.Output < 0.9998 && semiProductMap.Output > 1.0009)
                return (false, "semi-product should be made up per 1 kg of finished product");
            semiProductMap.ProductLists = productListMap;

            //Call the DAL update service
            var addSemiProduct = await _semiProductService.CreateSemiProductAsync(semiProductMap);
            if (!addSemiProduct.IsSuccess)
                return (false, addSemiProduct.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteSemiProductAsync(int semiProductId)
    {
        var deletedSemiProduct = await _semiProductService.DeleteSemiProductAsync(semiProductId);
        if (!deletedSemiProduct.IsSuccess)
            return (false, deletedSemiProduct.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuIngredientResponse> AllIngredients)> GetAllIngredientsAsync()
    {
        try
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            if (!ingredients.IsSuccess)
                return (false, ingredients.ErrorMessage, new List<MenuIngredientResponse>());

            //Mapping Products
            var ingredientMap = _mapper.Map<List<MenuIngredientResponse>>(ingredients.IngredientList);
            if (ingredientMap == null)
                return (false, "Mapping failed, object is null", new List<MenuIngredientResponse>());

            foreach (var ingredientResponse in ingredientMap)
            {
                ingredientResponse.ProductList = new List<MenuProductListResponse>();
                foreach (var ingredient in ingredients.IngredientList.Where(d => d.Id == ingredientResponse.Id))
                {
                    var semiProductUsedList = await _ingredientService.GetRelatedSemiProductsAsync(ingredient);
                    foreach (var semiProduct in semiProductUsedList.SemiProducts)
                    {
                        MenuProductListResponse prodList = new MenuProductListResponse();
                        prodList.Id = semiProduct.Id;
                        prodList.Name = semiProduct.Name;
                        prodList.PrimeCost = semiProduct.PrimeCost;
                        prodList.Weight = semiProduct.Weight;
                        
                        ingredientResponse.ProductList.Add(prodList);
                    }
                }
            }

            return (true, string.Empty, ingredientMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<MenuIngredientResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateIngredientAsync(MenuIngredientUpdateRequest updatedIngredient)
    {
        try
        {
            //Mapping Ingredients
            var ingredientMap = _mapper.Map<Ingredient>(updatedIngredient);
            if (ingredientMap == null)
                return (false, "Mapping failed, object is null", updatedIngredient.Id);

            var categoryId = await _ingredientCategoryService.GetCategoryIdByName(updatedIngredient.CategoryName);
            if (!categoryId.IsSuccess)
                return (false, categoryId.ErrorMessage, -1);

            ingredientMap.CategoryId = categoryId.CategoryId;

            //Call the DAL update service
            var updateIngredient = await _ingredientService.UpdateIngredientAsync(ingredientMap);
            if (!updateIngredient.IsSuccess)
                return (false, updateIngredient.ErrorMessage, -1);

            return (true, string.Empty, updatedIngredient.Id);
        }
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddIngredientAsync(MenuIngredientAddRequest ingredient)
    {
        try
        {
            //Mapping Ingredients
            var ingredientMap = _mapper.Map<Ingredient>(ingredient);
            if (ingredientMap == null)
                return (false, "Mapping failed, object is null");

            var categoryId = await _ingredientCategoryService.GetCategoryIdByName(ingredient.CategoryName);
            if (!categoryId.IsSuccess)
                return (false, categoryId.ErrorMessage);

            ingredientMap.CategoryId = categoryId.CategoryId;

            //Call the DAL update service
            var addIngredient = await _ingredientService.CreateIngredientAsync(ingredientMap);
            if (!addIngredient.IsSuccess)
                return (false, addIngredient.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientAsync(int ingredientId)
    {
        try
        {
            var deletedIngredient = await _ingredientService.DeleteIngredientAsync(ingredientId);
            if (!deletedIngredient.IsSuccess)
                return (false, deletedIngredient.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuProductCategoryResponse> AllProductCategories)> GetAllProductCategoriesAsync()
    {
        try
        {
            //Get Product Categories
            var productCategories = await _productCategoryService.GetAllProductCategoriesAsync();
            if (!productCategories.IsSuccess)
                return (false, productCategories.ErrorMessage, new List<MenuProductCategoryResponse>());

            //Map Product Categories
            var productCategoriesMap =
                _mapper.Map<List<MenuProductCategoryResponse>>(productCategories.ProductCategoryList);
            if (productCategoriesMap == null)
                return (false, "Mapping failed, object is null", new List<MenuProductCategoryResponse>());

            return (true, string.Empty, productCategoriesMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<MenuProductCategoryResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddProductCategoryAsync(MenuProductCategoryRequest productCategory)
    {
        try
        {
            //Map Product Category
            var productCategoriesMap = _mapper.Map<ProductCategory>(productCategory);
            if (productCategoriesMap == null)
                return (false, "Mapping failed, object is null");

            //Call the DAL update service
            var addProductCategory = await _productCategoryService.CreateProductCategoryAsync(productCategoriesMap);
            if (!addProductCategory.IsSuccess)
                return (false, addProductCategory.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductCategoryAsync(int productCategoryId)
    {
        try
        {
            var deletedProductCategory = await _productCategoryService.DeleteProductCategoryAsync(productCategoryId);
            if (!deletedProductCategory.IsSuccess)
                return (false, deletedProductCategory.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<MenuIngredientCategoryResponse> AllIngredientCategories)> GetAllIngredientCategoriesAsync()
    {
        try
        {
            //Get Ingredient Categories
            var ingredientCategories = await _ingredientCategoryService.GetAllIngredientCategoriesAsync();
            if (!ingredientCategories.IsSuccess)
                return (false, ingredientCategories.ErrorMessage, new List<MenuIngredientCategoryResponse>());

            //Map Product Categories
            var ingredientCategoriesMap =
                _mapper.Map<List<MenuIngredientCategoryResponse>>(ingredientCategories.IngredientCategoryList);
            if (ingredientCategoriesMap == null)
                return (false, "Mapping failed, object is null", new List<MenuIngredientCategoryResponse>());

            //Change required data
            foreach (var categoryResponse in ingredientCategoriesMap)
            {
                foreach (var category in ingredientCategories.IngredientCategoryList.Where(d =>
                             d.Id == categoryResponse.Id))
                {
                    categoryResponse.IngredientCount = category.Ingredients.Count;
                }
            }

            return (true, string.Empty, ingredientCategoriesMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<MenuIngredientCategoryResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddIngredientCategoryAsync(MenuIngredientCategoryRequest ingredientCategory)
    {
        try
        {
            //Map Ingredient Category
            var ingredientCategoriesMap = _mapper.Map<IngredientsCategory>(ingredientCategory);
            if (ingredientCategoriesMap == null)
                return (false, "Mapping failed, object is null");

            //Call the DAL update service
            var addIngredientCategory =
                await _ingredientCategoryService.CreateIngredientCategoryAsync(ingredientCategoriesMap);
            if (!addIngredientCategory.IsSuccess)
                return (false, addIngredientCategory.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientCategoryAsync(int ingredientCategoryId)
    {
        try
        {
            var deletedIngredientCategory =
                await _ingredientCategoryService.DeleteIngredientCategoryAsync(ingredientCategoryId);
            if (!deletedIngredientCategory.IsSuccess)
                return (false, deletedIngredientCategory.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    private static (bool IsSuccess, string ErrorMessage, double Weight) GetWeight(List<ProductList> productList)
    {
        try
        {
            double weight = 0;
            foreach (var product in productList)
            {
                weight += product.Count;
            }

            return (true, string.Empty, weight);
        }
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }
    
    private static (bool IsSuccess, string ErrorMessage, decimal PrimeCost) GetPrimeCost(List<ProductList> productList)
    {
        try
        {
            decimal primeCost = 0;
            foreach (var product in productList)
            {
                primeCost += product.PrimeCost * (decimal)product.Count;
            }

            return (true, string.Empty, primeCost);
        }
        catch (Exception e)
        {
            return (false, e.Message, 0);
        }
    }
}