using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with IngredientCategory table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class IngredientCategoryService : IIngredientCategoryService
{
    private readonly StoreHouseContext _context;
    public IngredientCategoryService(StoreHouseContext context) => _context = context;
 
    //Add IngredientCategory to Database
    public async Task<(bool IsSuccess, string ErrorMessage, IngredientsCategory IngredientsCategory)> CreateIngredientCategoryAsync(IngredientsCategory ingredientCategory)
    {
        await _context.IngredientsCategories.AddAsync(ingredientCategory);
        
        var saved = await _context.SaveChangesAsync();
        return saved == 0 ? 
                        (false, "Something went wrong when adding to db", ingredientCategory) : 
                        (true, string.Empty, ingredientCategory);
    }

    // Delete IngredientCategory from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientCategoryAsync(int ingredientCategoryId)
    {
        var ingredientCategory = await _context.IngredientsCategories.FirstOrDefaultAsync(d => d.Id == ingredientCategoryId);
        if (ingredientCategory == null) return (false, "Client does not exist");

        _context.IngredientsCategories.Remove(ingredientCategory);
        var saved = await _context.SaveChangesAsync();
        
        return saved == 0 ? 
                        (false, "Something went wrong when deleting from db") : 
                        (true, string.Empty);
    }

    //Get all IngredientCategories
    public async Task<(bool IsSuccess, string ErrorMessage, List<IngredientsCategory> IngredientCategoryList)> GetAllIngredientCategoriesAsync()
    {
        var ingredientsCategories = await _context.IngredientsCategories.ToListAsync();
        
        return (true, string.Empty, ingredientsCategories);
    }
}