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
 
    public Task<(bool IsSuccess, string ErrorMessage)> CreateProductCategoryAsync(ProductCategory productCategory)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductCategoryAsync(int productCategoryId)
    {
        //Delete Category and Related Products
        var productCategory = await _context.ProductCategories
                        .Include(p => p.Products)
                        .FirstOrDefaultAsync(d => d.Id == productCategoryId);
        if (productCategory == null) return (false, "Client does not exist");

        _context.ProductCategories.Remove(productCategory);
        var saved = await _context.SaveChangesAsync();
        
        return saved == 0 ? 
                        (false, "Something went wrong when deleting from db") : 
                        (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<ProductCategory> ProductCategoryList)> GetAllProductCategoriesAsync()
    {
        var productCategories = await _context.ProductCategories.ToListAsync();
        
        return (true, string.Empty, productCategories);
    }
}