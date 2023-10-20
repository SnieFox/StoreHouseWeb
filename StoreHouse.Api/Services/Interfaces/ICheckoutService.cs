using StoreHouse.Api.Model.DTO.CheckoutDTO;

namespace StoreHouse.Api.Services.Interfaces;

public interface ICheckoutService
{
    Task<(bool IsSuccess, string ErrorMessage, List<CheckoutProductCategoryResponse> AllProductCategories)> GetAllProductCategoriesAsync();
    Task<(bool IsSuccess, string ErrorMessage, List<CheckoutClientResponse> AllClients)> GetAllClientsAsync();
    Task<(bool IsSuccess, string ErrorMessage)> AddClientAsync(CheckoutClientRequest client);
    Task<(bool IsSuccess, string ErrorMessage, List<CheckoutReceiptResponse> AllReceipts)> GetAllReceiptsAsync();
    Task<(bool IsSuccess, string ErrorMessage)> AddReceiptAsync(CheckoutReceiptRequest receipt, string userLogin);
}