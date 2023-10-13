namespace StoreHouse.Api.Model.DTO.ManageDTO;

public class ManageUserRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string RoleName { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string MobilePhone { get; set; }
    public string PinCode { get; set; }
}