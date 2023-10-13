using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.ProductListDTO;

public class WriteOffProductListRequest
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Comment { get; set; }
    [Required]
    public double Count { get; set; }
}