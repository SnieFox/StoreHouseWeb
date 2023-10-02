namespace StoreHouse.Database.Entities;

public class ProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageId { get; set; }

    public List<Product> Products { get; set; } = new();
}