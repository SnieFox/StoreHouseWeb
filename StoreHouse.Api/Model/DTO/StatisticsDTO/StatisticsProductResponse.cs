namespace StoreHouse.Api.Model.DTO.StatisticsDTO;

public class StatisticsProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SoldCount { get; set; }
    public decimal Sum { get; set; }
}