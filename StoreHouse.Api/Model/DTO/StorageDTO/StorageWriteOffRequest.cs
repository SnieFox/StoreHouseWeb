using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageWriteOffRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Cause { get; set; }
    public string Comment { get; set; }
    public List<WriteOffProductListRequest> ProductList { get; set; }
}