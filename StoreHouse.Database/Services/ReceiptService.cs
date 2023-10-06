using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Extensions;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Receipt table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class ReceiptService : IReceiptService
{
    private readonly StoreHouseContext _context;
    public ReceiptService(StoreHouseContext context) => _context = context;
 
    //Create Receipt
    public async Task<(bool IsSuccess, string ErrorMessage, Receipt Receipt)> CreateReceiptAsync(Receipt receipt)
    {
        try
        {
            //Create Receipt
            await _context.Receipts.AddAsync(receipt);
            var saved = await _context.SaveChangesAsync();

            var updateResult =
                            await SupportingMethodExtension.UpdateRemainsAsync(_context, receipt.ProductLists, false);
            if (!updateResult.IsSuccess)
                return (false, $"Update of Remains Failed. {updateResult.ErrorMessage}", receipt);

            return saved == 0
                            ? (false, $"Something went wrong when deleting from db", receipt)
                            : (true, string.Empty, receipt);
        }
        catch (Exception e)
        {
            return (false, e.Message, receipt);
        }
    }

    //Remove Receipt
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteReceiptAsync(int receiptId)
    {
        try
        {
            //Remove Receipt
            var receipt = await _context.Receipts
                            .Include(c => c.ProductLists)
                            .FirstOrDefaultAsync(c => c.Id == receiptId);
            if (receipt == null) return (false, "Receipt does not exist");

            var updateResult = await SupportingMethodExtension.UpdateRemainsAsync(_context, receipt.ProductLists, true);
            if (!updateResult.IsSuccess) return (false, $"Update of Remains Failed. {updateResult.ErrorMessage}");

            _context.Receipts.Remove(receipt);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, $"Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    //Get all Receipts
    public async Task<(bool IsSuccess, string ErrorMessage, List<Receipt> ReceiptList)> GetAllReceiptsAsync()
    {
        try
        {
            var receipts = await _context.Receipts
                            .Include(c => c.ProductLists)
                            .ToListAsync();

            return (true, string.Empty, receipts);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Receipt>());
        }
    }
}