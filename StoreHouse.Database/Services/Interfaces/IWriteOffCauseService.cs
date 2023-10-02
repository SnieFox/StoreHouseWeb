using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with WriteOffCause table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IWriteOffCauseService
{
 //WriteOffCause methods
 Task<(bool IsSuccess, string ErrorMessage)> CreateWriteOffCauseAsync(WriteOffCause writeOffCause);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffCauseAsync(int writeOffCauseId);
 Task<(bool IsSuccess, string ErrorMessage, List<WriteOffCause> WriteOffCauseList)> GetAllWriteOffCausesAsync();
}