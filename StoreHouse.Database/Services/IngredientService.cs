using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Ingredient table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class IngredientService : IIngredientService
{
    private readonly StoreHouseContext _context;
    public IngredientService(StoreHouseContext context) => _context = context;
 
    //Create Ingredient 
    public async Task<(bool IsSuccess, string ErrorMessage, Ingredient ingredient)> CreateIngredientAsync(Ingredient ingredient)
    {
        //Create Ingredient
        await _context.Ingredients.AddAsync(ingredient);
        var saved = await _context.SaveChangesAsync();
                        
        return saved == 0 ? 
                        (false, $"Something went wrong when deleting from db", ingredient) : 
                        (true, string.Empty, ingredient);
    }

    //Update Ingredient
    public async Task<(bool IsSuccess, string ErrorMessage, Ingredient ingredient)> UpdateIngredientAsync(Ingredient updatedIngredient)
    {
        //Update Ingredient
        var ingredient = await _context.Ingredients
                        .FirstOrDefaultAsync(d => d.Id == updatedIngredient.Id);
        if (ingredient == null) return (false, "Ingredient does not exist", updatedIngredient);
        ingredient.Name = updatedIngredient.Name;
        ingredient.CategoryId = updatedIngredient.CategoryId;
        var saved = await _context.SaveChangesAsync();
        return saved == 0 ? 
                        (false, $"Something went wrong when updating Ingredient {updatedIngredient.Id} to db", updatedIngredient) : 
                        (true, string.Empty, updatedIngredient);
    }

    //Delete Ingredient
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientAsync(int ingredientId)
    {
        //Remove Ingredient
        var ingredient = await _context.Ingredients
                        .FirstOrDefaultAsync(c => c.Id == ingredientId);
        if (ingredient == null) return (false, "Client does not exist");
        _context.Ingredients.Remove(ingredient);
        var saved = await _context.SaveChangesAsync();
        
        return saved == 0 ? 
                        (false, $"Something went wrong when deleting from db") : 
                        (true, string.Empty);
    }

    //Get all Ingredients
    public async Task<(bool IsSuccess, string ErrorMessage, List<Ingredient> IngredientList)> GetAllIngredientsAsync()
    {
        var ingredients = await _context.Ingredients
                        .ToListAsync();
        
        return (true, string.Empty, ingredients);
    }
}