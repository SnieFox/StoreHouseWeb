using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Supplies table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class SupplyService : ISupplyService
{
 private readonly StoreHouseContext _context;
 public SupplyService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateSupplyAsync(Supply supply)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> UpdateSupplyAsync(Supply updatedSupply)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplyAsync(int supplyId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<Supply> SupplyList)> GetAllSuppliesAsync()
 {
  throw new NotImplementedException();
 }
}