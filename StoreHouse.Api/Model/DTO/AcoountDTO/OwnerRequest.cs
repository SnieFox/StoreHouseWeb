using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.AcoountDTO;

public class OwnerRequest
{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }

    [Required] public string RoleName { get; set; } = "Owner";
    [Required]
    public string Login { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    
    public string? Password { get; set; }
    [Required]
    [Phone]
    public string MobilePhone { get; set; }
    [Required]
    public string OrganizationName { get; set; }
    [Required]
    [StringLength(4, MinimumLength = 4)]
    [RegularExpression(@"^\d+$", ErrorMessage = "Можно использовать только цифры.")]
    public string PinCode { get; set; }
}