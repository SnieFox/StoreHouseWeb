using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Dishes table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IDishService
{
    //Dish methods
    Task<(bool IsSuccess, string ErrorMessage, Dish Dish)> CreateDishAsync(Dish dish);
    Task<(bool IsSuccess, string ErrorMessage, Dish UpdatedDish)> UpdateDishAsync(Dish updatedDish);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteDishAsync(int dishId);
    Task<(bool IsSuccess, string ErrorMessage, List<Dish> DishList)> GetAllDishesAsync();
    Task<(bool IsSuccess, string ErrorMessage, List<ProductList> ProductList)> GetProductListByName(string name);
}