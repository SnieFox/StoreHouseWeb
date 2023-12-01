using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.AcсountDTO;

public class OrganizationRequest
{
    [Required]
    public string Name { get; set; }
}