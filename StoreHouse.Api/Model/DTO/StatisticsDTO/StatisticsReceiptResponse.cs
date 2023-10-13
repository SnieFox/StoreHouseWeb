namespace StoreHouse.Api.Model.DTO.StatisticsDTO;

public class StatisticsReceiptResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Type { get; set; }
    public decimal Sum { get; set; }
}