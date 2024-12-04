using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using server.Data;
using server.Dtos;
using server.Dtos.Recipe;
using server.Interfaces;
using server.Models;
using server.Utlis;

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
                DishType = await DishType.GetDefaultDishType(dbContext),
                Reviews = new Reviews(),
                SpotPicture = new MediaFile(""),

            });

            await dbContext.SaveChangesAsync();

            return recipe.Entity;
        }

        public async Task<Recipe?> GetRecipeByIdAsync(Guid recipeId)
        {
            return await dbContext.Recipes
                .Include( recipe => recipe.Author)
                .Include( recipe => recipe.DishType)
                .Include( recipe => recipe.Ingredients)
                .Include( recipe => recipe.Instructions)
                    .ThenInclude( instr => instr.Media)
                .Include( recipe => recipe.SpotPicture)
                .Include( recipe => recipe.Comments).ThenInclude(comment => comment.CreatedBy)
                .Include( recipe => recipe.Reviews).ThenInclude(reviews => reviews.AllReviews)
                .Include( recipe => recipe.Tags)
                .FirstOrDefaultAsync(recipe => recipe.Id == recipeId);
        }

        public async Task<bool> IsOwner(Guid recipeId, User user)
        {
            var recipe = await dbContext.Recipes.Include(recipe => recipe.Author).FirstOrDefaultAsync(x => x.Id == recipeId);

            if (recipe == null) return false;

            return recipe.Author.Id == user.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipeDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MediaFileNotFoundException"></exception>
        public async Task<bool> UpdateRecipe(UpdateRecipeDto recipeDto, Guid id)
        {
            var recipe = await dbContext.Recipes
                .Include(r => r.Instructions).ThenInclude(x => x.Media)
                .Include(recipe => recipe.SpotPicture)
                .ThenInclude(mediaFile => mediaFile.CreatedBy)
                .Include(recipe => recipe.Author)
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Tags)
                .FirstOrDefaultAsync(r => r.Id == id);

            var dishType = await dbContext.DishTypes.FindAsync(recipeDto.DishTypeId);
             
            if (recipe == null || dishType == null) return false;

            recipe.Name = recipeDto.Name;
            recipe.Description = recipeDto.Description;
            recipe.DishType = dishType;

            //################## SPOT PICTURE ##################
            // If true than there is a new picture diffrent from old one
            if (
                recipe.SpotPicture.Id != recipeDto.SpotPicture.Id 
                )
            {
                // new picture should be already added so just get it from db
                var newMediaFile = await dbContext.MediaFiles.FindAsync(recipeDto.SpotPicture.Id) ?? throw new MediaFileNotFoundException();

                // remove old picture
                imageStorageService.DeleteImage(recipe.SpotPicture);
                dbContext.MediaFiles.Remove(recipe.SpotPicture);

                // Update the spot picture (if new null than pesista
                recipe.SpotPicture = newMediaFile;
                    
            }

            ////################## INSTRUCTION ##################
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
                    var newIntrstruction = await instructionService.Create(newInstructionDto, recipe.Author);
                    recipe.Instructions.Add(newIntrstruction);
                }
            }

            //################## INGREDIENTS ##################
            // Remove all ingredients
            dbContext.Ingredients.RemoveRange(recipe.Ingredients);

            // Add new ingredients
            if (recipeDto.Ingredients != null)
            {
                recipe.Ingredients.AddRange(recipeDto.Ingredients.Select(x => x.ToModel()));
            }

            //################## TAGS ##################
            // Remove all tags
            dbContext.Tags.RemoveRange(recipe.Tags);

            // Add new tags
            if (recipeDto.Tags != null)
            {
                recipe.Tags.AddRange(recipeDto.Tags.Select(x => x.ToModel()));
            }

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Recipe>> GetAllRecipesFilterAsync(Filter? filter, string userId)
        {
            IQueryable<Recipe> list = dbContext.Recipes
                .Include(recipe => recipe.Author)
                .Include(recipe => recipe.DishType)
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Instructions).ThenInclude(instr => instr.Media)
                .Include(recipe => recipe.SpotPicture)
                .Include(recipe => recipe.Comments).ThenInclude(comment => comment.CreatedBy)
                .Include(recipe => recipe.Reviews).ThenInclude(reviews => reviews.AllReviews)
                .Include(recipe => recipe.Tags)
               ;

            if (filter is null)
            {
                return await list.ToListAsync();
            }

            if (filter.NameFilter is not null)
            {
                list = list.Where(x => x.Name.Contains(filter.NameFilter));
            }

            if (filter.OnlyMy is not null && filter.OnlyMy == true)
            {
                list = list.Where(x => x.Author.Id == userId);
            }

            return await list.ToListAsync();
        }

        public async Task<bool> DeleteRecipe(Guid id)
        {
            var recipe = await GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return false;
            }

            dbContext.Recipes.Remove(recipe);

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
