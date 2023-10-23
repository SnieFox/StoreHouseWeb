using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.ManageDTO;

public class ManageRoleRequest
{
    [Required]
    [StringLength(15)]
    public string Name { get; set; }
}