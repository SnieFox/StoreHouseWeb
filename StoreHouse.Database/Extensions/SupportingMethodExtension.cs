using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreHouse.Database.Entities;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Extensions;

public static class SupportingMethodExtension
{
    //Method to Update Remains of Ingredients and Products
    public static async Task<(bool IsSuccess, string ErrorMessage)> UpdateRemainsAsync(this StoreHouseContext dbService, List<ProductList> productList, bool increase)
    {
        try
        {
            string errors = string.Empty;

            if (productList.Count == 0)
            {
                return (false, "ProductList is empty");
            }

            foreach (var prod in productList)
            {
                var ingredient = await dbService.Ingredients.FirstOrDefaultAsync(c => c.Name == prod.Name);
                var product = await dbService.Products.FirstOrDefaultAsync(c => c.Name == prod.Name);
                var semiProduct = await dbService.SemiProducts
                                .Include(c => c.ProductLists)
                                .FirstOrDefaultAsync(c => c.Name == prod.Name);
                var dish = await dbService.Dishes
                    .Include(d => d.ProductLists)
                    .FirstOrDefaultAsync(c => c.Name == prod.Name);

                if (ingredient != null)
                {
                    ingredient.Remains += increase ? prod.Count : -prod.Count;
                }
                else if (product != null)
                {
                    product.Remains += increase ? prod.Count : -prod.Count;
                }
                else if (semiProduct != null)
                {
                    var prodListToChange = new List<ProductList>();
                    foreach (var pr in semiProduct.ProductLists)
                    {
                        prodListToChange.Add(new ProductList
                        {
                            Id = pr.Id,
                            Name = pr.Name,
                            Price = pr.Price,
                            PrimeCost = pr.PrimeCost,
                            Comment = pr.Comment,
                            Count = pr.Count * prod.Count
                        });
                    }
                    var result = await UpdateRemainsAsync(dbService, prodListToChange, increase);
                    errors += $"SemiProduct id {semiProduct.Id}: {result.ErrorMessage}; ";
                }
                else if (dish != null)
                {
                    var prodListToChange = new List<ProductList>();
                    foreach (var pr in dish.ProductLists)
                    {
                        prodListToChange.Add(new ProductList
                        {
                            Id = pr.Id,
                            Name = pr.Name,
                            Price = pr.Price,
                            PrimeCost = pr.PrimeCost,
                            Comment = pr.Comment,
                            Count = pr.Count * prod.Count
                        });
                    }
                    var result = await UpdateRemainsAsync(dbService, prodListToChange, increase);
                    errors += $"Dish id {dish.Id}: {result.ErrorMessage}; ";
                }
                
                var saved = await dbService.SaveChangesAsync();

                if (saved == 0)
                {
                    errors += $"Something went wrong when updating {(ingredient != null ? "Ingredient" : (product != null ? "Product" : "SemiProduct"))} Remains to db; ";
                }
            }

            return (true, errors);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
    
}