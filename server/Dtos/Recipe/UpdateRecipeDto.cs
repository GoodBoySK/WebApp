using server.Models;
using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Recipe
{
    public class UpdateRecipeDto
    {
        [Required]
        public string Name { get; set; } = "Recipe";
        public string Description { get; set; } = string.Empty;
        [Required]
        public required int DishTypeId { get; set; }

        [Required]
        public int Time { get; set; } //In minutes
        [Required]
        [Range(0,Int32.MaxValue)]
        public int Portions { get; set; }
        [Required]
        [Range(0, 5)]
        public int Difficulty { get; set; }

        public MediaFileDto SpotPicture { get; set; } = new MediaFileDto();
        public List<IngredientDto>? Ingredients { get; set; }
        public List<InstructionDto>? Instructions { get; set; }
        public List<TagDto>? Tags { get; set; }
    }
}
