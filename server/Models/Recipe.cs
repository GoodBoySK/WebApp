using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Recipe
{
    public Guid Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = "Recipe";
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public int Time { get; set; }// In minutes
    public int Portions { get; set; }
    public int Difficulty { get; set; }
    public required DishType DishType { get; set; }
    public required MediaFile SpotPicture { get; set; }
    public List<Ingredient> Ingredients { get; set; } = [];
    public List<Instruction> Instructions { get; set; } = [];
    public required User Author { get; set; }
    public required Reviews Reviews { get; set; }
    public List<Comment> Comments { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];

    public Recipe()
    {
        
    }

    public Recipe(User author, DishType dishType)
    {
        DishType = dishType;
        Author = author;
    }
    
}