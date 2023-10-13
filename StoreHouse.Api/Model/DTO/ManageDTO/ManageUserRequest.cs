using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.ManageDTO;

public class ManageUserRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    public string RoleName { get; set; }
    [Required]
    public string Login { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Phone]
    public string MobilePhone { get; set; }
    [Required]
    public string PinCode { get; set; }
}