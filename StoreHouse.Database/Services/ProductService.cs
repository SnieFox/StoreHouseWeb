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
        try
        {
            //Create Product
            await _context.Products.AddAsync(product);
            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when deleting from db", product)
                            : (true, string.Empty, product);
        }
        catch (Exception e)
        {
            return (false, e.Message, product);
        }
    }

    //Update Product
    public async Task<(bool IsSuccess, string ErrorMessage, Product Product)> UpdateProductAsync(Product updatedProduct)
    {
        try
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
            return saved == 0
                            ? (false, $"Something went wrong when updating Product {updatedProduct.Id} to db",
                                            updatedProduct)
                            : (true, string.Empty, updatedProduct);
        }
        catch (Exception e)
        {
            return (false, e.Message, updatedProduct);
        }
    }

    //Remove Product
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(int productId)
    {
        try
        {
            //Remove Product
            var product = await _context.Products
                            .FirstOrDefaultAsync(c => c.Id == productId);
            if (product == null) return (false, "Product does not exist");
            _context.Products.Remove(product);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, $"Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<Product> ProductList)> GetAllProductsAsync()
    {
        try
        {
            var products = await _context.Products
                .Include(c => c.Category)
                .ToListAsync();

            return (true, string.Empty, products);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Product>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<ProductList> ProductList)> GetProductListByNameAsync(string name)
    {
        try
        {
            var productsList = await _context.ProductLists
                .Where(p => p.Name == name && p.ReceiptId != 0)
                .ToListAsync();

            return (true, string.Empty, productsList);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<ProductList>());
        }
    }
    
    //Get PrimeCost by Name
    public async Task<(bool IsSuccess, string ErrorMessage, decimal PrimeCost)> GetPrimeCostByName(string name)
    {
        if (!await _context.Products.AnyAsync(i => i.Name == name))
            return (false, "No product with this name", -1);

        var primeCost = await _context.Products
            .Where(p => p.Name == name)
            .Select(p => p.PrimeCost)
            .FirstOrDefaultAsync();
        if (primeCost == 0)
            return (false, "No Product with this name", primeCost);
        return (true, string.Empty, primeCost);
    }
}