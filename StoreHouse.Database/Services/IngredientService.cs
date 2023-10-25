using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.DTO;
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
    public async Task<(bool IsSuccess, string ErrorMessage, Ingredient Ingredient)> CreateIngredientAsync(Ingredient ingredient)
    {
        try
        {
            //Create Ingredient
            await _context.Ingredients.AddAsync(ingredient);
            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when deleting from db", ingredient)
                            : (true, string.Empty, ingredient);
        }
        catch (Exception e)
        {
            return (false, e.Message, ingredient);
        }
    }

    //Update Ingredient
    public async Task<(bool IsSuccess, string ErrorMessage, Ingredient Ingredient)> UpdateIngredientAsync(Ingredient updatedIngredient)
    {
        try
        {
            //Update Ingredient
            var ingredient = await _context.Ingredients
                            .FirstOrDefaultAsync(d => d.Id == updatedIngredient.Id);
            if (ingredient == null) return (false, "Ingredient does not exist", updatedIngredient);
            ingredient.Name = updatedIngredient.Name;
            ingredient.CategoryId = updatedIngredient.CategoryId;
            var saved = await _context.SaveChangesAsync();
            return saved == 0
                            ? (false, $"Something went wrong when updating Ingredient {updatedIngredient.Id} to db",
                                            updatedIngredient)
                            : (true, string.Empty, updatedIngredient);
        }
        catch (Exception e)
        {
            return (false, e.Message, updatedIngredient);
        }
    }

    //Delete Ingredient
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientAsync(int ingredientId)
    {
        try
        {
            //Remove Ingredient
            var ingredient = await _context.Ingredients
                            .FirstOrDefaultAsync(c => c.Id == ingredientId);
            if (ingredient == null) return (false, "Ingredient does not exist");
            _context.Ingredients.Remove(ingredient);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, $"Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    //Get all Ingredients
    public async Task<(bool IsSuccess, string ErrorMessage, List<Ingredient> IngredientList)> GetAllIngredientsAsync()
    {
        try
        {
            var ingredients = await _context.Ingredients
                .Include(c => c.Category)
                .ToListAsync();

            return (true, string.Empty, ingredients);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Ingredient>());
        }
    }
    
    //Get PrimeCost by Name
    public async Task<(bool IsSuccess, string ErrorMessage, decimal PrimeCost)> GetPrimeCostByName(string name)
    {
        if (!await _context.Ingredients.AnyAsync(i => i.Name == name))
            return (false, "No ingredient with this name", -1);

        var primeCost = await _context.Ingredients
            .Where(i => i.Name == name)
            .Select(i => i.PrimeCost)
            .FirstOrDefaultAsync();
        return (true, string.Empty, primeCost);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<RelatedSemiProductsDTO> SemiProducts)> GetRelatedSemiProductsAsync(
        Ingredient ingredient)
    {
        var semiProductResponse = new List<RelatedSemiProductsDTO>();

        var productLists = await _context.ProductLists
            .Where(p => p.SemiProductId != null && p.Name == ingredient.Name)
            .ToListAsync();

        foreach (var product in productLists)
        {
            var semiProduct = await _context.SemiProducts
                .Where(s => s.Id == product.SemiProductId).FirstOrDefaultAsync();
            
            semiProductResponse.Add(new RelatedSemiProductsDTO
            {
                Id = semiProduct.Id,
                Name = semiProduct.Name,
                Weight = product.Count,
                PrimeCost = product.PrimeCost
            });
        }

        return (true, string.Empty, semiProductResponse);
    }
}