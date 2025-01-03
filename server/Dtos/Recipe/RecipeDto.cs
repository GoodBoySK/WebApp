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

    public int Time {get; set; } //In minutes
    public int Portions {get; set; }
    public int Difficulty { get; set; }
    
    public required DishType DishType { get; set; }
    public MediaFileDto? SpotPicture { get; set; }
    public List<IngredientDto> Ingredients { get; set; } = [];
    public List<InstructionDto> Instructions { get; set; } = [];
    public required User Author { get; set; }
    public Reviews Reviews { get; set; } = new ();
    public List<CommentDto> Comments { get; set; } = [];
    public List<TagDto> Tags { get; set; } = [];
    

    }

    
}