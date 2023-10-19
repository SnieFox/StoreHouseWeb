using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with ProductCategory table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IProductCategoryService
{
 //ProductCategory methods
 Task<(bool IsSuccess, string ErrorMessage, ProductCategory ProductCategory)> CreateProductCategoryAsync(ProductCategory productCategory);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteProductCategoryAsync(int productCategoryId);
 Task<(bool IsSuccess, string ErrorMessage, List<ProductCategory> ProductCategoryList)> GetAllProductCategoriesAsync();
 Task<(bool IsSuccess, string ErrorMessage, int CategoryId)> GetCategoryIdByNameAsync(string name);
}