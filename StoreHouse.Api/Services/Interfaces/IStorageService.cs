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
    Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplierAsync(int supplierId);
    Task<(bool IsSuccess, string ErrorMessage)> AddSupplierAsync(StorageSupplierRequest supplier);
    Task<(bool IsSuccess, string ErrorMessage, List<StorageSupplierResponse> AllSuppliers)> GetAllSuppliersAsync();
    
    
    //CRUD for WriteOffs
    Task<(bool IsSuccess, string ErrorMessage, List<StorageWriteOffResponse> AllWriteOffs)> GetAllWriteOffsAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateWriteOffAsync(StorageWriteOffRequest updatedWriteOff);
    Task<(bool IsSuccess, string ErrorMessage)> AddWriteOffAsync(StorageWriteOffRequest writeOff, string userLogin);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffAsync(int writeOffId);

    Task<(bool IsSuccess, string ErrorMessage, List<StorageWriteOffCauseResponse> AllWriteOffCauses)> GetAllWriteOffCausesAsync();
    Task<(bool IsSuccess, string ErrorMessage)> AddWriteOffCauseAsync(StorageWriteOffCauseRequest writeOffCause);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffCauseAsync(int writeOffCauseId);
}