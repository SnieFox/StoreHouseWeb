using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with DishesCategory table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class DishCategoryService : IDishCategoryService
{
    private readonly StoreHouseContext _context;
    public DishCategoryService(StoreHouseContext context) => _context = context;
 
    //Add DishesCategory to Database
    public async Task<(bool IsSuccess, string ErrorMessage, DishesCategory DishesCategory)> CreateDishCategoryAsync(DishesCategory dishCategory)
    {
        try
        {
            await _context.DishesCategories.AddAsync(dishCategory);

            var saved = await _context.SaveChangesAsync();
            return saved == 0
                            ? (false, "Something went wrong when adding to db", dishCategory)
                            : (true, string.Empty, dishCategory);
        }
        catch (Exception e)
        {
            return (false, e.Message, dishCategory);
        }
    }

    //Delete DishesCategory from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteDishCategoryAsync(int dishCategoryId)
    {
        try
        {
            var dishCategory = await _context.DishesCategories
                            .Include(d => d.Dishes)
                            .FirstOrDefaultAsync(d => d.Id == dishCategoryId);
            if (dishCategory == null) return (false, "Client does not exist");

            _context.DishesCategories.Remove(dishCategory);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    //Get all DishesCategories from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<DishesCategory> DishCategoryList)> GetAllDishCategoriesAsync()
    {
        try
        {
            var dishesCategories = await _context.DishesCategories.ToListAsync();

            return (true, string.Empty, dishesCategories);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<DishesCategory>());
        }
    }
}