namespace StoreHouse.Database.Entities;

public class Organization
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public List<User>? Users { get; set; }
}