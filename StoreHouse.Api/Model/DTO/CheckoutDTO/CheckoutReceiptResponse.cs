using StoreHouse.Api.Model.DTO.ProductListDTO;
using StoreHouse.Database.Entities;

namespace StoreHouse.Api.Model.DTO.CheckoutDTO;

public class CheckoutReceiptResponse
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string UserName { get; set; }
    public string ClientName { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }
    public List<ReceiptProductListResponse> ProductList { get; set; }
}