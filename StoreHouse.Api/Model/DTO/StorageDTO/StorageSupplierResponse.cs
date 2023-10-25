namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageSupplierResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MobilePhone { get; set; }
    public string Comment { get; set; }
    public int SuppliesCount { get; set; }
    public decimal SupplySum { get; set; }
}