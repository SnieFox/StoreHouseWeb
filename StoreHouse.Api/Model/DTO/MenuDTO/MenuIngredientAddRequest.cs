using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuIngredientAddRequest
{
    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string Name { get; set; }
    [Required]
    public string CategoryName { get; set; }
    [Required]
    [RegularExpression("^(шт\\.|л\\.|кг\\.)$", ErrorMessage = "Allowable units: 'кг.', 'л.', 'шт.'")]
    public string Unit { get; set; }
    [Required]
    public double Remains { get; set; }
    [Required]
    public decimal PrimeCost { get; set; }
}