using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Recipe
{
    public Guid Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = "Recipe";
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    public DishType DishType { get; set; }
    public MediaFile? SpotPicture { get; set; }
    public Ingredient[]? Ingredients { get; set; }
    public Instruction[]? Instructions { get; set; }
    public User Author { get; set; }
    public Reviews Reviews { get; set; } = new ();
    public Comment[]? Comments { get; set; }
    public Tag[]? Tags { get; set; }

    public Recipe(User _author, DishType dishType)
    {
        Author = _author;
        DishType = dishType;
    }
}