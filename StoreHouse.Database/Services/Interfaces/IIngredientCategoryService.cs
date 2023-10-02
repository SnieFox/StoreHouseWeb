using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with IngredientCategory table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IIngredientCategoryService
{
 //IngredientCategory methods
 Task<(bool IsSuccess, string ErrorMessage, IngredientsCategory IngredientsCategory)> CreateIngredientCategoryAsync(IngredientsCategory ingredientCategory);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientCategoryAsync(int ingredientCategoryId);
 Task<(bool IsSuccess, string ErrorMessage, List<IngredientsCategory> IngredientCategoryList)> GetAllIngredientCategoriesAsync();
}