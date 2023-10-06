using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Supplier table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class SupplierService : ISupplierService
{
    private readonly StoreHouseContext _context;
    public SupplierService(StoreHouseContext context) => _context = context;
 
    //Add Supplier to Database
    public async Task<(bool IsSuccess, string ErrorMessage, Supplier Supplier)> CreateSupplierAsync(Supplier supplier)
    {
        try
        {
            await _context.Suppliers.AddAsync(supplier);

            var saved = await _context.SaveChangesAsync();
            return saved == 0
                            ? (false, "Something went wrong when adding to db", supplier)
                            : (true, string.Empty, supplier);
        }
        catch (Exception e)
        {
            return (false, e.Message, supplier);
        }
    }

    //Update Supplier in Database
    public async Task<(bool IsSuccess, string ErrorMessage, Supplier Supplier)> UpdateSupplierAsync(Supplier updatedSupplier)
    {
        try
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(c => c.Id == updatedSupplier.Id);
            if (supplier == null) return (false, "Client does not exist", updatedSupplier);

            supplier.Name = updatedSupplier.Name;
            supplier.MobilePhone = updatedSupplier.MobilePhone;
            supplier.Comment = updatedSupplier.Comment;
            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when updating Client {updatedSupplier.Id} to db",
                                            updatedSupplier)
                            : (true, string.Empty, updatedSupplier);
        }
        catch (Exception e)
        {
            return (false, e.Message, updatedSupplier);
        }
    }

    //Delete Supplier from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplierAsync(int supplierId)
    {
        try
        {
            var supplier = await _context.Suppliers
                            .Include(c => c.Supplies)
                            .FirstOrDefaultAsync(c => c.Id == supplierId);
            if (supplier == null) return (false, "Client does not exist");

            _context.Suppliers.Remove(supplier);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    //Get all Supplier from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<Supplier> SupplierList)> GetAllSuppliersAsync()
    {
        try
        {
            var suppliers = await _context.Suppliers.ToListAsync();

            return (true, string.Empty, suppliers);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Supplier>());
        }
    }
}