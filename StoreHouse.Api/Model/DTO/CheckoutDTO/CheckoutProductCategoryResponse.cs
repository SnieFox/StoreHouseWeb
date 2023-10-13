using StoreHouse.Api.Model.DTO.DishListDTO;

namespace StoreHouse.Api.Model.DTO.CheckoutDTO;

public class CheckoutProductCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageId { get; set; }
    public List<DishListResponse> DishList { get; set; }
}