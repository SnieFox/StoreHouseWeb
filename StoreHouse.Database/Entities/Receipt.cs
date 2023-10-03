namespace StoreHouse.Database.Entities;

public class Receipt
{
    public int Id { get; set; }
    public int? ClientId { get; set; }
    public int? UserId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }

    public Client? Client { get; set; }
    public User? User { get; set; }
    public List<ProductList> ProductLists { get; set; } = new();
}