namespace StoreHouse.Api.Model.DTO.StatisticsDTO;

public class StatisticsEmployeeResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public decimal ReceiptSum { get; set; }
    public int ReceiptsCount { get; set; }
    public decimal AverageReceiptSum { get; set; }
}