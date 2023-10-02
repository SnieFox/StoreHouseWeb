using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with SemiProduct table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class SemiProductService : ISemiProductService
{
 private readonly StoreHouseContext _context;
 public SemiProductService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateSemiProductAsync(SemiProduct semiProduct)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> UpdateSemiProductAsync(SemiProduct updatedSemiProduct)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteSemiProductAsync(int semiProductId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<SemiProduct> SemiProductList)> GetAllSemiProductsAsync()
 {
  throw new NotImplementedException();
 }
}