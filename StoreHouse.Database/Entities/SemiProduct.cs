namespace StoreHouse.Database.Entities;

public class SemiProduct
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Output { get; set; }
    public decimal PrimeCost { get; set; }
    public string Prescription { get; set; } = string.Empty;

    public List<ProductList>? ProductLists { get; set; }
}
