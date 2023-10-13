namespace StoreHouse.Api.Model.DTO.ProductListDTO;

public class WriteOffProductListResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public decimal Count { get; set; }
    public decimal Sum { get; set; }
}