using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Supplier table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class SupplierService : ISupplierService
{
 private readonly StoreHouseContext _context;
 public SupplierService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateSupplierAsync(Supplier supplier)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> UpdateSupplierAsync(Supplier updatedSupplier)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplierAsync(int supplierId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<Supplier> SupplierList)> GetAllSuppliersAsync()
 {
  throw new NotImplementedException();
 }
}