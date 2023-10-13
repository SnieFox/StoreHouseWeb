using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.ManageDTO;

public class ManageClientRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string FullName { get; set; }
    [CreditCard]
    public string BankCard { get; set; }
    [Required]
    [Phone]
    public string MobilePhone { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Comment { get; set; }
    public string Address { get; set; }
}