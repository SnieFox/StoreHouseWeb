using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuIngredientResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string Unit { get; set; }
    public double Remains { get; set; }
    public decimal PrimeCost { get; set; }
    public List<MenuProductListResponse> ProductList { get; set; }
}