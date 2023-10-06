using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Extensions;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Supplies table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class SupplyService : ISupplyService
{
    private readonly StoreHouseContext _context;
    public SupplyService(StoreHouseContext context) => _context = context;
 
    //Add Supply to Database
    public async Task<(bool IsSuccess, string ErrorMessage, Supply Supply)> CreateSupplyAsync(Supply supply)
    {
        try
        {
            await _context.Supplies.AddAsync(supply);

            var updateResult = await SupportingMethodExtension.UpdateRemainsAsync(_context, supply.ProductLists, true);
            if (!updateResult.IsSuccess)
                return (false, $"Update of Remains Failed. {updateResult.ErrorMessage}", supply);

            var saved = await _context.SaveChangesAsync();
            return saved == 0
                            ? (false, "Something went wrong when adding to db", supply)
                            : (true, string.Empty, supply);
        }
        catch (Exception e)
        {
            return (false, e.Message, supply);
        }
    }

    //Update Supply in Database
    public async Task<(bool IsSuccess, string ErrorMessage, Supply Supply)> UpdateSupplyAsync(Supply updatedSupply)
    {
        try
        {
            var supply = await _context.Supplies.FirstOrDefaultAsync(c => c.Id == updatedSupply.Id);
            if (supply == null) return (false, "Supply does not exist", updatedSupply);

            supply.SupplierId = updatedSupply.SupplierId;
            supply.UserName = updatedSupply.UserName;
            supply.Date = updatedSupply.Date;
            supply.Sum = updatedSupply.Sum;
            supply.Comment = updatedSupply.Comment;
            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when updating Supply {updatedSupply.Id} to db",
                                            updatedSupply)
                            : (true, string.Empty, updatedSupply);
        }
        catch (Exception e)
        {
            return (false, e.Message, updatedSupply);
        }
    }

    //Delete Supply from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplyAsync(int supplyId)
    {
        try
        {
            var supply = await _context.Supplies
                            .Include(c => c.ProductLists)
                            .FirstOrDefaultAsync(c => c.Id == supplyId);
            if (supply == null) return (false, "Supply does not exist");

            var updateResult = await SupportingMethodExtension.UpdateRemainsAsync(_context, supply.ProductLists, false);
            if (!updateResult.IsSuccess) return (false, $"Update of Remains Failed. {updateResult.ErrorMessage}");

            _context.Supplies.Remove(supply);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    //Get all Supplies from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<Supply> SupplyList)> GetAllSuppliesAsync()
    {
        try
        {
            var supplies = await _context.Supplies.ToListAsync();

            return (true, string.Empty, supplies);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Supply>());
        }
    }
}