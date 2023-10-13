using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageSupplyResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string SupplierName { get; set; }
    public decimal Sum { get; set; }
    public List<SupplyProductListResponse> ProductList { get; set; }
}