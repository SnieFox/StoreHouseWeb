using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuProductRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 chars")]
    public string Name { get; set; }
    [Required]
    public string CategoryName { get; set; }
    [Required]
    public string ImageId { get; set; }
    [Required]
    public decimal PrimeCost { get; set; }
    [Required]
    public decimal Price { get; set; }
}