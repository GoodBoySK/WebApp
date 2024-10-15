using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos;
using server.Models;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipeController(AppDbContext dbContext, UserManager<User> userManager) : ControllerBase
{
    [HttpGet("{recipeId}")]
    public async Task<IActionResult> Get(Guid recipeId)
    {
        var recipe = await dbContext.Recipes.FindAsync(recipeId);

        if (recipe == null)
        {
            return NotFound();
        }
            
        return Ok(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRecipeDTO recipeDTO)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return BadRequest("Logged user not found in db.");
        }


        Recipe instance = new Recipe()
        {
            Author = user,
            Name = recipeDTO.Name,
        };

        var recipe = await dbContext.Recipes.AddAsync(instance);
        await dbContext.SaveChangesAsync();
        return CreatedAtRoute("GetRecipe", recipe.Entity.Id , null);
    }


}