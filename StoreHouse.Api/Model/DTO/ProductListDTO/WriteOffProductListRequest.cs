namespace StoreHouse.Api.Model.DTO.ProductListDTO;

public class WriteOffProductListRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public double Count { get; set; }
}