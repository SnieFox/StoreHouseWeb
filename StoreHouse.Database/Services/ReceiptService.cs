using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Receipt table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class ReceiptService : IReceiptService
{
 private readonly StoreHouseContext _context;
 public ReceiptService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateReceiptAsync(Receipt receipt)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteReceiptAsync(int receiptId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<Receipt> ReceiptList)> GetAllReceiptsAsync()
 {
  throw new NotImplementedException();
 }
}