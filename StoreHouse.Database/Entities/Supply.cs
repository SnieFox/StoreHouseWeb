namespace StoreHouse.Database.Entities;

public class Supply
{
    public int Id { get; set; }
    public int? SupplierId { get; set; }
    public int? UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Sum { get; set; }
    public string Comment { get; set; } = string.Empty;
    
    public Supplier? Supplier { get; set; }
    public User? User { get; set; }
    public List<ProductList>? ProductLists { get; set; }
}