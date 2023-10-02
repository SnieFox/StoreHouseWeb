using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with DishesCategory table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IDishCategoryService
{
 //DishCategory methods
 Task<(bool IsSuccess, string ErrorMessage, DishesCategory DishesCategory)> CreateDishCategoryAsync(DishesCategory dishCategory);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteDishCategoryAsync(int dishCategoryId);
 Task<(bool IsSuccess, string ErrorMessage, List<DishesCategory> DishCategoryList)> GetAllDishCategoriesAsync();
}