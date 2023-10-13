namespace StoreHouse.Api.Model.DTO.ManageDTO;

public class ManageUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string PinCode { get; set; }
    public string RoleName { get; set; }
    public DateTime LastLoginDate { get; set; }
}