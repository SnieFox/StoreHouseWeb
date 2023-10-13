namespace StoreHouse.Api.Model.DTO.ManageDTO;

public class ManageClientResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string BankCard { get; set; }
    public string MobilePhone { get; set; }
    public decimal ReceiptSum { get; set; }
}