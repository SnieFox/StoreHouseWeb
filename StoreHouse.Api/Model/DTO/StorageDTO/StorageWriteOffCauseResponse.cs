namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageWriteOffCauseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int WriteOffCount { get; set; }
    public decimal WriteOffSum { get; set; }
}