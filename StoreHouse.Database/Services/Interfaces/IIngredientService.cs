using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Ingredient table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IIngredientService
{
 //Ingredient methods
 Task<(bool IsSuccess, string ErrorMessage, Ingredient ingredient)> CreateIngredientAsync(Ingredient ingredient);
 Task<(bool IsSuccess, string ErrorMessage, Ingredient ingredient)> UpdateIngredientAsync(Ingredient updatedIngredient);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientAsync(int ingredientId);
 Task<(bool IsSuccess, string ErrorMessage, List<Ingredient> IngredientList)> GetAllIngredientsAsync();
}