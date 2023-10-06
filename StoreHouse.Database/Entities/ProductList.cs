namespace StoreHouse.Database.Entities;

public class ProductList
{
    public int Id { get; set; }
    public int? DishId { get; set; }
    public int? SemiProductId { get; set; }
    public int? WriteOffId { get; set; }
    public int? SupplyId { get; set; }
    public int? ReceiptId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Count { get; set; }
    public decimal Sum { get; set; }
    public decimal PrimeCost { get; set; }
    public string Comment { get; set; } = string.Empty;
    
    public Dish? Dish { get; set; }
    public SemiProduct? SemiProduct { get; set; }
    public WriteOff? WriteOff { get; set; }
    public Supply? Supply { get; set; }
    public Receipt? Receipt { get; set; }
}