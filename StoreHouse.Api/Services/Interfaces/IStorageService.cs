using StoreHouse.Api.Model.DTO.StorageDTO;

namespace StoreHouse.Api.Services.Interfaces;

public interface IStorageService
{
    //Get All Remains
    Task<(bool IsSuccess, string ErrorMessage, List<StorageRemainResponse> AllRemains)> GetAllRemainsAsync();
    
    //CRUD for Supplies
    Task<(bool IsSuccess, string ErrorMessage, List<StorageSupplyResponse> AllSupplies)> GetAllSuppliesAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateSupplyAsync(StorageSupplyRequest updatedSupply);
    Task<(bool IsSuccess, string ErrorMessage)> AddSupplyAsync(StorageSupplyRequest supply, string userLogin);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplyAsync(int supplyId);
    
    //CRUD for WriteOffs
    Task<(bool IsSuccess, string ErrorMessage, List<StorageWriteOffResponse> AllriteOffs)> GetAllWriteOffsAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateWriteOffAsync(StorageWriteOffRequest updatedWriteOff);
    Task<(bool IsSuccess, string ErrorMessage)> AddWriteOffAsync(StorageWriteOffRequest supply);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffAsync(int supplyId);
}