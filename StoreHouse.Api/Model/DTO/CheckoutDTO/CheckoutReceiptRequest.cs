using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.CheckoutDTO;

public class CheckoutReceiptRequest
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string UserName { get; set; }
    public string ClientName { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }
    public List<ReceiptProductListRequest> ProductList { get; set; }
}