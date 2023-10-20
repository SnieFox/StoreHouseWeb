using System.ComponentModel.DataAnnotations;
using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuDishRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    public string CategoryName { get; set; }
    [Required]
    public string ImageId { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public List<MenuProductListRequest> ProductList { get; set; }
}