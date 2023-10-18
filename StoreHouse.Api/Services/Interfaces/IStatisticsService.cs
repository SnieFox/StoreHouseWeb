using StoreHouse.Api.Model.DTO.StatisticsDTO;
using StoreHouse.Database.Entities;

namespace StoreHouse.Api.Services.Interfaces;

public interface IStatisticsService
{
    Task<(bool IsSuccess, string ErrorMessage, List<StatisticsClientResponse> AllClients)> GetAllClientsAsync();
    Task<(bool IsSuccess, string ErrorMessage, List<StatisticsEmployeeResponse> AllEmployee)> GetAllEmployeesAsync();
    Task<(bool IsSuccess, string ErrorMessage, List<StatisticsProductResponse> AllProduct)> GetAllProductsAsync();
    Task<(bool IsSuccess, string ErrorMessage, List<StatisticsReceiptResponse> AllReceipt)> GetAllReceiptsAsync();
}