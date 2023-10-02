namespace StoreHouse.Database.Entities;

public class DishesCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageId { get; set; }

    public List<Dish> Dishes { get; set; } = new();
}