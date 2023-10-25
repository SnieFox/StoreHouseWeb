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

            var updateResult = await _context.UpdateRemainsAsync(supply.ProductLists, true);
            if (!updateResult.IsSuccess)
                return (false, $"Update of Remains Failed. {updateResult.ErrorMessage}", supply);


            return (true, string.Empty, supply);
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
            var existingSupply = await _context.Supplies
                .Include(s => s.ProductLists)
                .FirstOrDefaultAsync(s => s.Id == updatedSupply.Id);
            if (existingSupply == null)
                return (false, "Supply not found", updatedSupply);
            
            existingSupply.SupplierId = updatedSupply.SupplierId;
            existingSupply.Date = updatedSupply.Date;
            existingSupply.Sum = updatedSupply.Sum;
            existingSupply.Comment = updatedSupply.Comment;
            
            //Откат изменений
            if (updatedSupply.ProductLists.Count != 0)
            {
                var refundResult = await _context.UpdateRemainsAsync(existingSupply.ProductLists, false);
                if (!refundResult.IsSuccess) return (false, $"Update of Remains Failed. {refundResult.ErrorMessage}", updatedSupply);

                foreach (var productList in existingSupply.ProductLists)
                {
                    _context.ProductLists.Remove(productList);
                }
                
                await _context.SaveChangesAsync();
            }
            
            var updateResult = await _context.UpdateRemainsAsync(updatedSupply.ProductLists, true);
            if (!updateResult.IsSuccess) return (false, $"Update of Remains Failed. {updateResult.ErrorMessage}", updatedSupply);

            foreach (var productList in updatedSupply.ProductLists)
            {
                productList.SupplyId = existingSupply.Id;
                _context.ProductLists.Add(productList);
            }
            
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db", updatedSupply) : (true, string.Empty, updatedSupply);
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

            var updateResult = await _context.UpdateRemainsAsync(supply.ProductLists, false);
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
            var supplies = await _context.Supplies
                .Include(c => c.Supplier)
                .Include(p => p.ProductLists)
                .ToListAsync();

            return (true, string.Empty, supplies);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Supply>());
        }
    }
}