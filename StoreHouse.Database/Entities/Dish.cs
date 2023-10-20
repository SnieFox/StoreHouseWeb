namespace StoreHouse.Database.Entities;

public class Dish
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageId { get; set; } = string.Empty;
    
    public ProductCategory? Category { get; set; }
    public List<ProductList> ProductLists { get; set; } = new();
}