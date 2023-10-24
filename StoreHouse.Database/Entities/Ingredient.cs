namespace StoreHouse.Database.Entities;

public class Ingredient
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal PrimeCost { get; set; }
    public double Remains { get; set; }

    public IngredientsCategory? Category { get; set; }
}