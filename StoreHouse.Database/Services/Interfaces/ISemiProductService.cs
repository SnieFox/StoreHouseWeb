using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with SemiProduct table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface ISemiProductService
{
 //SemiProduct methods
 Task<(bool IsSuccess, string ErrorMessage, SemiProduct SemiProduct)> CreateSemiProductAsync(SemiProduct semiProduct);
 Task<(bool IsSuccess, string ErrorMessage, SemiProduct SemiProduct)> UpdateSemiProductAsync(SemiProduct updatedSemiProduct);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteSemiProductAsync(int semiProductId);
 Task<(bool IsSuccess, string ErrorMessage, List<SemiProduct> SemiProductList)> GetAllSemiProductsAsync();
 Task<(bool IsSuccess, string ErrorMessage, decimal PrimeCost)> GetPrimeCostByName(string name);
}