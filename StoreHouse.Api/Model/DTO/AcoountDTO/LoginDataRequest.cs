using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.AcoountDTO;

public class LoginDataRequest
{
    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string Login { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string Password { get; set; }
}