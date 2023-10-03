namespace StoreHouse.Database.Entities;

public class WriteOff
{
    public int Id { get; set; }
    public int? CauseId { get; set; }
    public int? UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Comment { get; set; } = string.Empty;

    public User? User { get; set; }
    public WriteOffCause? Cause { get; set; }
    public List<ProductList> ProductLists { get; set; } = new();
}