namespace StoreHouse.Api.Model.DTO.StatisticsDTO;

public class StatisticsClientResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string MobilePhone { get; set; }
    public decimal ByCardSum { get; set; }
    public decimal ByCashSum { get; set; }
    public int ReceiptsCount { get; set; }
    public decimal AverageReceiptSum { get; set; }
}