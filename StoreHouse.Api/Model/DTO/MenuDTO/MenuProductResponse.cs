namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuProductResponse
{
    public int Id { get; set; }
    public string ImageId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PrimeCost { get; set; }
    public decimal Price { get; set; }
}