using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuIngredientCategoryRequest
{
    [Required]
    [StringLength(15, MinimumLength = 2)]
    public string Name { get; set; }
}