using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with SemiProduct table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class SemiProductService : ISemiProductService
{
    private readonly StoreHouseContext _context;
    public SemiProductService(StoreHouseContext context) => _context = context;
 
    //Add SemiProduct to Database
    public async Task<(bool IsSuccess, string ErrorMessage, SemiProduct SemiProduct)> CreateSemiProductAsync(SemiProduct semiProduct)
    {
        await _context.SemiProducts.AddAsync(semiProduct);
        
        var saved = await _context.SaveChangesAsync();
        return saved == 0 ? 
                        (false, "Something went wrong when adding to db", semiProduct) : 
                        (true, string.Empty, semiProduct);
    }

    //Update SemiProduct in Database
    public async Task<(bool IsSuccess, string ErrorMessage, SemiProduct SemiProduct)> UpdateSemiProductAsync(SemiProduct updatedSemiProduct)
    {
        var semiProduct = await _context.SemiProducts.FirstOrDefaultAsync(c => c.Id == updatedSemiProduct.Id);
        if (semiProduct == null) return (false, "SemiProduct does not exist", updatedSemiProduct);
        
        semiProduct.Name = updatedSemiProduct.Name;
        semiProduct.Output = updatedSemiProduct.Output;
        semiProduct.PrimeCost = updatedSemiProduct.PrimeCost;
        semiProduct.Prescription = updatedSemiProduct.Prescription;
        var saved = await _context.SaveChangesAsync();
        
        return saved == 0 ? 
                        (false, $"Something went wrong when updating SemiProduct {updatedSemiProduct.Id} to db", updatedSemiProduct) : 
                        (true, string.Empty, updatedSemiProduct);
    }

    //Delete SemiProduct from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteSemiProductAsync(int semiProductId)
    {
        var semiProduct = await _context.SemiProducts
                        .Include(c => c.ProductLists)
                        .FirstOrDefaultAsync(c => c.Id == semiProductId);
        if (semiProduct == null) return (false, "Client does not exist");

        _context.SemiProducts.Remove(semiProduct);
        var saved = await _context.SaveChangesAsync();
        
        return saved == 0 ? 
                        (false, "Something went wrong when deleting from db") : 
                        (true, string.Empty);
    }

    //Get all SemiProduct from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<SemiProduct> SemiProductList)> GetAllSemiProductsAsync()
    {
        var semiProducts = await _context.SemiProducts
                        .Include(c => c.ProductLists)
                        .ToListAsync();
        
        return (true, string.Empty, semiProducts);
    }
}