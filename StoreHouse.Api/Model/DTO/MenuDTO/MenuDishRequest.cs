using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuDishRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public double Weight { get; set; }
    public decimal PrimeCost { get; set; }
    public decimal Price { get; set; }
    public List<MenuProductListRequest> ProductList { get; set; }
}