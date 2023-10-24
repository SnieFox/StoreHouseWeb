namespace StoreHouse.Database.Entities;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageId { get; set; }
    public decimal PrimeCost { get; set; }
    public decimal Price { get; set; }
    public double Remains { get; set; }

    public ProductCategory? Category { get; set; }
}