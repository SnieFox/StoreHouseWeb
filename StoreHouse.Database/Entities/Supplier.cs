namespace StoreHouse.Database.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;

    public List<Supply> Supplies { get; set; } = new();
}