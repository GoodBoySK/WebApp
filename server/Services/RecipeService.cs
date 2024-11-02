using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using server.Data;
using server.Dtos;
using server.Dtos.Recipe;
using server.Interfaces;
using server.Models;

namespace server.Services
{
    public class RecipeService(AppDbContext dbContext, IImageStorageService imageStorageService, IInstructionService instructionService) : IRecipeService
    {
        public async Task<Recipe> CreateRecipeAsync(CreateRecipeDto createRecipeDto, User loggedUser)
        {
            
            var recipe = await dbContext.Recipes.AddAsync(new Recipe
            {
                Name = createRecipeDto.Name,
                Author = loggedUser,
                DishType = await DishType.GetDefaultDishType(dbContext)
            });

            await dbContext.SaveChangesAsync();

            return recipe.Entity;
        }

        public async Task<Recipe?> GetRecipeByIdAsync(Guid recipeId)
        {
            return await dbContext.Recipes
                .Include( recipe => recipe.Author )
                .Include( recipe => recipe.Ingredients)
                .Include( recipe => recipe.Instructions)
                .Include( recipe => recipe.SpotPicture)
                .Include( recipe => recipe.Comments)
                .Include( recipe => recipe.Reviews)
                .Include( recipe => recipe.Tags)
                .FirstOrDefaultAsync(recipe => recipe.Id == recipeId);
        }

        public async Task<bool> UpdateRecipe(UpdateRecipeDto recipeDto, Guid id)
        {
            var recipe = await dbContext.Recipes
                .Include(r => r.Instructions)
                .Include(recipe => recipe.SpotPicture)
                .Include(recipe => recipe.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            var dishType = await dbContext.DishTypes.FindAsync(recipeDto.DishType.Id);

            if (recipe == null || dishType == null) return false;




            recipe.Name = recipeDto.Name;
            recipe.Description = recipeDto.Description;
            recipe.DishType = dishType;

            //################## SPOT PICTURE ##################
            if (
                (recipe.SpotPicture is not null 
                 && recipeDto.SpotPicture is not null 
                 && recipe.SpotPicture?.Id != recipeDto.SpotPicture?.Id) 
                || (recipe.SpotPicture is null && recipeDto.SpotPicture is null))
            {
                MediaFile? newMediaFile = null;
                // If there is a old picture remove it
                if (recipe.SpotPicture != null)
                {
                    imageStorageService.DeleteImage(recipe.SpotPicture);
                }
                // If there is a new picture add it
                if (recipeDto.SpotPicture?.Image != null)
                {
                    newMediaFile = await imageStorageService.StoreImageAsync(recipeDto.SpotPicture.Image);
                } 
                // Update the spot picture (if new null than pesistant also null)
                recipe.SpotPicture = newMediaFile;
                    
            }

            ////################## INGREDIENTS ##################
            // Remove deleted instructions
            foreach (var oldInstruction in recipe.Instructions)
            {
                instructionService.Delete(oldInstruction);
            }

            // add new instructions if there are any 
            if (recipeDto.Instructions == null)
            {
                recipe.Instructions = [];
            }
            else
            {
                recipe.Instructions ??= [];
                foreach (var newInstructionDto in recipeDto.Instructions)
                {
                    var newIntrstruction = await instructionService.Create(newInstructionDto);
                    recipe.Instructions.Add(newIntrstruction);
                }
            }

            //################## INGREDIENTS ##################
            // Remove all ingredients
            recipe.Ingredients.Clear();

            // Add new ingredients
            if (recipeDto.Ingredients != null)
            {
                recipe.Ingredients.AddRange(recipeDto.Ingredients.Select(x => x.ToModel()));
            }

            //################## TAGS ##################
            // Remove all tags
            recipe.Tags.Clear();

            // Add new tags
            if (recipeDto.Tags != null)
            {
                recipe.Tags.AddRange(recipeDto.Tags.Select(x => x.ToModel()));
            }

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Recipe>> GetAllRecipesFilterAsync(Filter? filter)
        {
            if (filter == null)
            {
                return await dbContext.Recipes.ToListAsync();
            }

            if (filter.NameFilter == null)
            {
                return await dbContext.Recipes.ToListAsync();
            }
            return await dbContext.Recipes.Where(r => r.Name.Contains(filter.NameFilter)).ToListAsync();
        }
    }
}
