using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Product table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class ProductService : IProductService
{
 private readonly StoreHouseContext _context;
 public ProductService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateProductAsync(Product product)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> UpdateProductAsync(Product updatedProduct)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(int productId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<Product> ProductList)> GetAllProductsAsync()
 {
  throw new NotImplementedException();
 }
}