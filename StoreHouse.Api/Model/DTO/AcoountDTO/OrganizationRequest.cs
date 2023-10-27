using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.AcoountDTO;

public class OrganizationRequest
{
    [Required]
    public string Name { get; set; }
}