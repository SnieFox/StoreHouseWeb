namespace StoreHouse.Database.Entities;

public class Client
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Sex { get; set; } = "Not Selected";
    public string BankCard { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<Receipt> Receipts { get; set; } = new();
}