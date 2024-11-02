using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace server.Dtos.Recipe
{
    public class RecipeDto
    {
        
    public Guid Id { get; set; }
    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(500)]
    public required string Description { get; set; }
    
    public required DishType DishType { get; set; }
    public MediaFile? SpotPicture { get; set; }
    public List<Ingredient> Ingredients { get; set; } = [];
    public List<Instruction> Instructions { get; set; } = [];
    public required User Author { get; set; }
    public Reviews Reviews { get; set; } = new ();
    public List<Comment> Comments { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];
    

    }

    
}