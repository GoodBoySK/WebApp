using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos;
using server.Dtos.Recipe;
using server.Models;

namespace server.Utlis
{
    public static class RecipeExtensions
    {
        public static RecipeDto ToDto(this Recipe recipe) =>
            new()
            {
                Author = recipe.Author,
                Description = recipe.Description,
                Difficulty = recipe.Difficulty,
                DishType = recipe.DishType,
                Name = recipe.Name,
                Comments = recipe.Comments.Select(x => x.ToDto()).ToList(),
                Id = recipe.Id,
                Ingredients = recipe.Ingredients.Select(x => x.ToDto()).ToList(),
                Instructions = recipe.Instructions.Select(x => x.ToDto()).ToList(),
                Reviews = recipe.Reviews,
                SpotPicture = recipe.SpotPicture.ToDto(),
                Tags = recipe.Tags.Select(x => x.ToDto()).ToList(),
                Time = recipe.Time,
                Portions = recipe.Portions,
            };
        
        public static CommentDto ToDto(this Comment comment) =>
        new() 
        {
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            Parent = comment.Parent,
        };

        public static MediaFileDto ToDto(this MediaFile mediaFile) =>
        new() 
        {
            Id = mediaFile.Id,
        };    

        public static InstructionDto ToDto(this Instruction instruction) =>
        new()
        {
            Description = instruction.Description,
            Media = instruction.Media.ToDto() ,
            Position = instruction.Position,
        };

        public static IngredientDto ToDto(this Ingredient ingredient) =>
        new()
        {
            Name = ingredient.Name,
        };

        public static TagDto ToDto(this Tag tag) =>
        new()
        {
            Name = tag.Name,
        };
    }
    
}