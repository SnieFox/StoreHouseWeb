using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Product table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IProductService
{
 //Product methods
 Task<(bool IsSuccess, string ErrorMessage)> CreateProductAsync(Product product);
 Task<(bool IsSuccess, string ErrorMessage)> UpdateProductAsync(Product updatedProduct);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(int productId);
 Task<(bool IsSuccess, string ErrorMessage, List<Product> ProductList)> GetAllProductsAsync();
}