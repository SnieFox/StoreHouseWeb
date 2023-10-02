using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Represents a service for working with WriteOffCause table.
 * The methods of writing, reading and modifying table data are implemented.
 */
public class WriteOffCauseService : IWriteOffCauseService
{
 private readonly StoreHouseContext _context;
 public WriteOffCauseService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateWriteOffCauseAsync(WriteOffCause writeOffCause)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffCauseAsync(int writeOffCauseId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<WriteOffCause> WriteOffCauseList)> GetAllWriteOffCausesAsync()
 {
  throw new NotImplementedException();
 }
}