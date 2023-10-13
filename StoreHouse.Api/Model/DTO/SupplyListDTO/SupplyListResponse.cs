namespace StoreHouse.Api.Model.DTO.SupplyListDTO;

public class SupplyListResponse
{
    public int Id { get; set; }
    public DateTime SupplyDate { get; set; }
    public string Supplier { get; set; }
    public double Count { get; set; }
    public decimal PrimeCost { get; set; }
    public decimal Sum { get; set; }
}