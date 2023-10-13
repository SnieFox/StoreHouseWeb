namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuIngredientAddRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string Unit { get; set; }
    public double Remains { get; set; }
    public decimal PrimeCost { get; set; }
}