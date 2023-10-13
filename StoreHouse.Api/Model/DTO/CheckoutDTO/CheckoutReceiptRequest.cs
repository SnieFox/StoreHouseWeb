using System.ComponentModel.DataAnnotations;
using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.CheckoutDTO;

public class CheckoutReceiptRequest
{
    public int Id { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public string UserName { get; set; }
    public string? ClientName { get; set; }
    [Required]
    public DateTime OpenDate { get; set; }
    [Required]
    public DateTime CloseDate { get; set; }
    [Required]
    public List<ReceiptProductListRequest> ProductList { get; set; }
}