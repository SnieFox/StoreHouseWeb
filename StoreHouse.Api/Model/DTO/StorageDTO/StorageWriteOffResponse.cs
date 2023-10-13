using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageWriteOffResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string UserName { get; set; }
    public string Cause { get; set; }
    public decimal Sum { get; set; }
    public List<WriteOffProductListResponse> ProductList { get; set; }
}