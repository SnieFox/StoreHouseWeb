using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.ManageDTO;

public class ManageRoleRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(15)]
    public string Name { get; set; }
}