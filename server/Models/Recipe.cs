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
    public List<Ingredient> Ingredients { get; set; } = [];
    public List<Instruction> Instructions { get; set; } = [];
    public User Author { get; set; }
    public Reviews Reviews { get; set; } = new ();
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