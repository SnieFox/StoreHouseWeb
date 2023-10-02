using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Supplies table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface ISupplyService
{
 //Supply methods
 Task<(bool IsSuccess, string ErrorMessage)> CreateSupplyAsync(Supply supply);
 Task<(bool IsSuccess, string ErrorMessage)> UpdateSupplyAsync(Supply updatedSupply);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplyAsync(int supplyId);
 Task<(bool IsSuccess, string ErrorMessage, List<Supply> SupplyList)> GetAllSuppliesAsync();
}