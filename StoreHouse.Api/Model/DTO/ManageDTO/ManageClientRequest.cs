namespace StoreHouse.Api.Model.DTO.ManageDTO;

public class ManageClientRequest
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string BankCard { get; set; }
    public string MobilePhone { get; set; }
    public string Email { get; set; }
    public string Comment { get; set; }
    public string Address { get; set; }
}