using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageSupplyRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string SupplierName { get; set; }
    public string Comment { get; set; }
    public List<SupplyProductListRequest> ProductList { get; set; }
}