using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Comments = recipe.Comments,
                Id = recipe.Id,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                Reviews = recipe.Reviews,
                SpotPicture = recipe.SpotPicture,
                Tags = recipe.Tags,
                Time = recipe.Time,
                Portions = recipe.Portions,
            };
        
    }
}