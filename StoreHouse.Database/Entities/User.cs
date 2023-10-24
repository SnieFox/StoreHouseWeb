namespace StoreHouse.Database.Entities;

public class User
{
    public int Id { get; set; }
    public int? RoleId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public DateTime LastLoginDate { get; set; }
    public string PinCode { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    
    public Role? Role { get; set; }
    public List<Receipt>? Receipts { get; set; }
    public List<WriteOff>? WriteOffs { get; set; }
    public List<Supply>? Supplies { get; set; }
}