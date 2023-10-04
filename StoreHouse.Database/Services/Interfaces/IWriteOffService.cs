using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with WriteOff table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IWriteOffService
{
 //WriteOff methods
 Task<(bool IsSuccess, string ErrorMessage, WriteOff WriteOff)> CreateWriteOffAsync(WriteOff writeOff);
 Task<(bool IsSuccess, string ErrorMessage, WriteOff WriteOff)> UpdateWriteOffAsync(WriteOff updatedWriteOff);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffAsync(int writeOffId);
 Task<(bool IsSuccess, string ErrorMessage, List<WriteOff> WriteOffList)> GetAllWriteOffsAsync();
}