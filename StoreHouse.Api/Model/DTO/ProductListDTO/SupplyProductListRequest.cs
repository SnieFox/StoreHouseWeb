using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.ProductListDTO;

public class SupplyProductListRequest
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public double Count { get; set; }
}