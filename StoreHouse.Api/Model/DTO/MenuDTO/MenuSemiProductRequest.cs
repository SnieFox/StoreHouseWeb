using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuSemiProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public List<MenuProductListRequest> ProductList { get; set; }
}