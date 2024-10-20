using server.Models;
using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Recipe
{
    public class UpdateRecipeDto
    {
        public string Name { get; set; } = "Recipe";
        public string Description { get; set; } = string.Empty;
        public DishType DishType { get; set; }
        public MediaFileDto? SpotPicture { get; set; }
        public List<IngredientDto>? Ingredients { get; set; }
        public List<InstructionDto>? Instructions { get; set; }
        public List<TagDto>? Tags { get; set; }
    }
}
