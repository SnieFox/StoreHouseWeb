using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Product table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class ProductService : IProductService
{
    private readonly StoreHouseContext _context;
    public ProductService(StoreHouseContext context) => _context = context;
 
    //Create Product
    public async Task<(bool IsSuccess, string ErrorMessage, Product Product)> CreateProductAsync(Product product)
    {
        //Create Product
        await _context.Products.AddAsync(product);
        var saved = await _context.SaveChangesAsync();
                        
        return saved == 0 ? 
                        (false, $"Something went wrong when deleting from db", product) : 
                        (true, string.Empty, product);
    }

    //Update Product
    public async Task<(bool IsSuccess, string ErrorMessage, Product Product)> UpdateProductAsync(Product updatedProduct)
    {
        //Update Product
        var product = await _context.Products
                        .FirstOrDefaultAsync(d => d.Id == updatedProduct.Id);
        if (product == null) return (false, "Product does not exist", updatedProduct);
        product.Name = updatedProduct.Name;
        product.ImageId = updatedProduct.ImageId;
        product.PrimeCost = updatedProduct.PrimeCost;
        product.Price = updatedProduct.Price;
        product.CategoryId = updatedProduct.CategoryId;
        var saved = await _context.SaveChangesAsync();
        return saved == 0 ? 
                        (false, $"Something went wrong when updating Product {updatedProduct.Id} to db", updatedProduct) : 
                        (true, string.Empty, updatedProduct);
    }

    //Remove Product
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(int productId)
    {
        //Remove Product
        var product = await _context.Products
                        .FirstOrDefaultAsync(c => c.Id == productId);
        if (product == null) return (false, "Product does not exist");
        _context.Products.Remove(product);
        var saved = await _context.SaveChangesAsync();
        
        return saved == 0 ? 
                        (false, $"Something went wrong when deleting from db") : 
                        (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<Product> ProductList)> GetAllProductsAsync()
    {
        var products = await _context.Products
                        .ToListAsync();
        
        return (true, string.Empty, products);
    }
}