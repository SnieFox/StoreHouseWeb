using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with WriteOff table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class WriteOffService : IWriteOffService
{
 private readonly StoreHouseContext _context;
 public WriteOffService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateWriteOffAsync(WriteOff writeOff)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> UpdateWriteOffAsync(WriteOff updatedWriteOff)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffAsync(int writeOffId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<WriteOff> WriteOffList)> GetAllWriteOffsAsync()
 {
  throw new NotImplementedException();
 }
}