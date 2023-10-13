using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuIngredientUpdateRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    public string CategoryName { get; set; }
}