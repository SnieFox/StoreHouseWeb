namespace StoreHouse.Database.Entities;

public class User
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string HashedLogin { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public DateTime LastLoginDate { get; set; }
    public int PinCode { get; set; }
    public string Email { get; set; } = string.Empty;
    
    public Role? Role { get; set; }
    public List<Receipt> Receipts { get; set; } = new();
    public List<WriteOff> WriteOffs { get; set; } = new();
    public List<Supply> Supplies { get; set; } = new();
}