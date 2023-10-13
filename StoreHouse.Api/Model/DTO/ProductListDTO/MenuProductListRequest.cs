using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.ProductListDTO;

public class MenuProductListRequest
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public double Weight { get; set; }
}