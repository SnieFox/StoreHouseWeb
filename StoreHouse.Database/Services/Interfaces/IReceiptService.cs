using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Receipt table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IReceiptService
{
 //Receipt methods
 Task<(bool IsSuccess, string ErrorMessage, Receipt Receipt)> CreateReceiptAsync(Receipt receipt);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteReceiptAsync(int receiptId);
 Task<(bool IsSuccess, string ErrorMessage, List<Receipt> ReceiptList)> GetAllReceiptsAsync();
}