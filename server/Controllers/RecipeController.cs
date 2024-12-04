using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos;
using server.Dtos.Recipe;
using server.Interfaces;
using server.Models;
using server.Utlis;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipeController(IRecipeService recipeService, UserManager<User> userManager) : ControllerBase
{
    [HttpGet("{recipeId}")]
    public async Task<IActionResult> Get(Guid recipeId)
    {
        var recipe = await recipeService.GetRecipeByIdAsync(recipeId);

        if (recipe is null)
        {
            return NotFound();
        }
            
        return Ok(recipe.ToDto());
    }

    [HttpPost("all")]
    public async Task<IActionResult> GetAllWithFilter([FromBody]Filter filter)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null && filter.OnlyMy == true)
        {
            return Unauthorized();
        }

        var recipes = await recipeService.GetAllRecipesFilterAsync(filter, userId);
        return Ok(recipes);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRecipeDto recipeDTO)
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


        var recipe = await recipeService.CreateRecipeAsync(recipeDTO, user);
        return Ok(recipe.ToDto());
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateRecipeDto updateItemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        if (!await recipeService.IsOwner(id,(await userManager.FindByIdAsync(userId))!))
        {
            return Forbid();
        }

        await recipeService.UpdateRecipe(updateItemDto, id);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        if (!await recipeService.IsOwner(id, (await userManager.FindByIdAsync(userId))!))
        {
            return Forbid();
        }

        var hasDeletedRecipe = await recipeService.DeleteRecipe(id);

        if (!hasDeletedRecipe)
        {
            return BadRequest();
        }

        return NoContent();
    }
}