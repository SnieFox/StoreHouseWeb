using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuSemiProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public decimal PrimeCost { get; set; }
    public string Comment { get; set; }
    public List<MenuProductListResponse> ProductList { get; set; }
}