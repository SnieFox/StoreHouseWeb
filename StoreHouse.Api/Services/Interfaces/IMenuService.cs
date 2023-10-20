using StoreHouse.Api.Model.DTO.MenuDTO;

namespace StoreHouse.Api.Services.Interfaces;

public interface IMenuService
{
    //CRUD for Products
    Task<(bool IsSuccess, string ErrorMessage, List<MenuProductResponse> AllProducts)> GetAllProductsAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateProductAsync(MenuProductRequest updatedProduct);
    Task<(bool IsSuccess, string ErrorMessage)> AddProductAsync(MenuProductRequest product);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(int productId);
    
    //CRUD for Dishes
    Task<(bool IsSuccess, string ErrorMessage, List<MenuDishResponse> AllDishes)> GetAllDishesAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateDishAsync(MenuDishRequest updatedDish);
    Task<(bool IsSuccess, string ErrorMessage)> AddDishAsync(MenuDishRequest dish);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteDishAsync(int dishId);
    
    //CRUD for SemiProduct
    Task<(bool IsSuccess, string ErrorMessage, List<MenuSemiProductResponse> AllSemiProducts)> GetAllSemiProductsAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateSemiProductAsync(MenuSemiProductRequest updatedSemiProduct);
    Task<(bool IsSuccess, string ErrorMessage)> AddSemiProductAsync(MenuSemiProductRequest semiProduct);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteSemiProductAsync(int semiProductId);
    
    //CRUD for Ingredient
    Task<(bool IsSuccess, string ErrorMessage, List<MenuIngredientResponse> AllIngredients)> GetAllIngredientsAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateIngredientAsync(MenuIngredientUpdateRequest updatedIngredient);
    Task<(bool IsSuccess, string ErrorMessage)> AddIngredientAsync(MenuIngredientAddRequest ingredient);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientAsync(int ingredientId);
    
    //CRUD for ProductCategory
    Task<(bool IsSuccess, string ErrorMessage, List<MenuProductCategoryResponse> AllProductCategories)> GetAllProductCategoriesAsync();
    Task<(bool IsSuccess, string ErrorMessage)> AddProductCategoryAsync(MenuProductCategoryRequest productCategory);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteProductCategoryAsync(int productCategoryId);
    
    //CRUD for IngredientCategory
    Task<(bool IsSuccess, string ErrorMessage, List<MenuIngredientCategoryResponse> AllIngredientCategories)> GetAllIngredientCategoriesAsync();
    Task<(bool IsSuccess, string ErrorMessage)> AddIngredientCategoryAsync(MenuIngredientCategoryRequest ingredientCategory);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientCategoryAsync(int ingredientCategoryId);
}