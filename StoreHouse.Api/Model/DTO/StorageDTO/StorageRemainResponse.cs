using StoreHouse.Api.Model.DTO.SupplyListDTO;

namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageRemainResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string CategoryName { get; set; }
    public double Remains { get; set; }
    public decimal PrimeCost { get; set; }
    public decimal Sum { get; set; }
    public List<SupplyListResponse> SupplyList { get; set; }
}