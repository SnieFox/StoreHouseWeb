using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Dishes table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class DishService : IDishService
{
    private readonly StoreHouseContext _context;
    public DishService(StoreHouseContext context) => _context = context;
 
    //Create Dish and related ProductList to Database
    public async Task<(bool IsSuccess, string ErrorMessage, Dish Dish)> CreateDishAsync(Dish dish)
    {
        try
        {
            //Create Dish
            await _context.Dishes.AddAsync(dish);
            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when deleting from db", dish)
                            : (true, string.Empty, dish);
        }
        catch (Exception e)
        {
            return (false, e.Message, dish);
        }
    }

    //Update Dish and related ProductList in Database
    public async Task<(bool IsSuccess, string ErrorMessage, Dish UpdatedDish)> UpdateDishAsync(Dish updatedDish)
    {
        try
        {
            //Update Dish
            var dish = await _context.Dishes
                            .Include(d => d.ProductLists)
                            .FirstOrDefaultAsync(d => d.Id == updatedDish.Id);
            if (dish == null) return (false, "Dish does not exist", updatedDish);
            dish.Name = updatedDish.Name;
            dish.Price = updatedDish.Price;
            dish.CategoryId = updatedDish.CategoryId;

            //Update related ProductList
            foreach (var product in updatedDish.ProductLists)
            {
                var existingProduct = dish.ProductLists.FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct != null)
                {
                    existingProduct.SemiProductId = product.SemiProductId;
                    existingProduct.WriteOffId = product.WriteOffId;
                    existingProduct.SupplyId = product.SupplyId;
                    existingProduct.ReceiptId = product.ReceiptId;
                    existingProduct.Name = product.Name;
                    existingProduct.Count = product.Count;
                    existingProduct.Price = product.Price;
                    existingProduct.PrimeCost = product.PrimeCost;
                    existingProduct.Comment = product.Comment;
                }
                else
                {
                    dish.ProductLists.Add(product);
                }
            }

            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when updating Dish {updatedDish.Id} to db", updatedDish)
                            : (true, string.Empty, updatedDish);
        }
        catch (Exception e)
        {
            return (false, e.Message, updatedDish);
        }
    }

    //Delete Dish and Cascade Delete on ProductList
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteDishAsync(int dishId)
    {
        try
        {
            //Remove Dish
            var dish = await _context.Dishes
                            .Include(d => d.ProductLists)
                            .FirstOrDefaultAsync(c => c.Id == dishId);
            if (dish == null) return (false, "Client does not exist");
            _context.Dishes.Remove(dish);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, $"Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    // Get All Dishes with ProductLists
    public async Task<(bool IsSuccess, string ErrorMessage, List<Dish> DishList)> GetAllDishesAsync()
    {
        try
        {
            var dishes = await _context.Dishes
                            .Include(d => d.ProductLists)
                            .ToListAsync();

            return (true, string.Empty, dishes);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Dish>());
        }
    }
}