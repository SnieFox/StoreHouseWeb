using System.ComponentModel.DataAnnotations;
using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuSemiProductRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string Name { get; set; }
    public string Comment { get; set; }
    [Required]
    public List<MenuProductListRequest> ProductList { get; set; }
}