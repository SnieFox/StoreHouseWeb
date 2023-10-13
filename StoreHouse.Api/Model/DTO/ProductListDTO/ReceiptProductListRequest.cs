using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.ProductListDTO;

public class ReceiptProductListRequest
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Count { get; set; }
    [Required]
    public decimal Price { get; set; }
}