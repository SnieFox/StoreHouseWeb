using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with ProductCategory table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class ProductCategoryService : IProductCategoryService
{
    private readonly StoreHouseContext _context;
    public ProductCategoryService(StoreHouseContext context) => _context = context;
 
    public async Task<(bool IsSuccess, string ErrorMessage, ProductCategory ProductCategory)> CreateProductCategoryAsync(ProductCategory productCategory)
    {
        try
        {
            await _context.ProductCategories.AddAsync(productCategory);

            var saved = await _context.SaveChangesAsync();
            return saved == 0
                            ? (false, "Something went wrong when adding to db", productCategory)
                            : (true, string.Empty, productCategory);
        }
        catch (Exception e)
        {
            return (false, e.Message, productCategory);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductCategoryAsync(int productCategoryId)
    {
        try
        {
            //Delete Category and Related Products
            var productCategory = await _context.ProductCategories
                            .Include(p => p.Products)
                            .FirstOrDefaultAsync(d => d.Id == productCategoryId);
            if (productCategory == null) return (false, "Client does not exist");

            _context.ProductCategories.Remove(productCategory);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<ProductCategory> ProductCategoryList)> GetAllProductCategoriesAsync()
    {
        try
        {
            var productCategories = await _context.ProductCategories.ToListAsync();

            return (true, string.Empty, productCategories);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<ProductCategory>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int CategoryId)> GetCategoryIdByNameAsync(string name)
    {
        if (!await _context.ProductCategories.AnyAsync(s => s.Name == name))
            return (false, "No product category with this name", -1);

        var id = await _context.ProductCategories
            .Where(s => s.Name == name)
            .Select(s => s.Id)
            .FirstOrDefaultAsync();

        return (true, string.Empty, id);
    }
}