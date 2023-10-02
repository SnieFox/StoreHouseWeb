using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Database.Entities;

public class IngredientsCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageId { get; set; }

    public List<Ingredient> Ingredients { get; set; } = new();
}