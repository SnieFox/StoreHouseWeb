using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.MenuDTO;

public class MenuProductCategoryRequest
{
    public string ImageId { get; set; }
    [Required]
    [StringLength(15, MinimumLength = 1)]
    public string Name { get; set; }
}