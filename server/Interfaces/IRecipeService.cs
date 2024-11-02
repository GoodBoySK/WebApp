using server.Dtos;
using server.Dtos.Recipe;
using server.Models;

namespace server.Interfaces;

public interface IRecipeService
{
    Task<Recipe> CreateRecipeAsync(CreateRecipeDto createRecipeDto, User loggedUser);
    Task<Recipe?> GetRecipeByIdAsync(Guid recipeId);
    Task<bool> UpdateRecipe(UpdateRecipeDto recipeDto, Guid id);
    Task<ICollection<Recipe>> GetAllRecipesFilterAsync(Filter? filter);
}