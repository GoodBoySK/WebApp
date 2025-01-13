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
public class RecipeController(IRecipeService recipeService, UserManager<User> userManager, ICommentService commentService) : ControllerBase
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
    public async Task<IActionResult> GetAllWithFilter([FromBody]Filter? filter)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (filter != null && userId == null && filter.OnlyMy == true)
        {
            return Unauthorized();
        }

        var recipes = await recipeService.GetAllRecipesFilterAsync(filter, userId);
        return Ok(new {recipes = recipes.Item1.Select(x => x.ToDto()), allCount = recipes.Item2});
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
            return BadRequest(new ErrorMesage{Message = "Logged user not found in db."});
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
            return BadRequest(Utils.ValidationError(ModelState));
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
            return BadRequest(Utils.ValidationError(ModelState));
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
            return BadRequest(new ErrorMesage{Message = "Failed to delete recipe!!!"});
        }

        return NoContent();
    }

    [Authorize]
    [HttpPost("{id}/comment")]
    public async Task<IActionResult> AddComment(Guid id, [FromBody] AddCommentDto commentDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest(new ErrorMesage { Message = "Logged user not found in db." });
        }
        var recipe = await recipeService.GetRecipeByIdAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        
        var comment = await commentService.CreateCommentAsync(recipe, user, commentDto);

        return Ok(comment.ToDto());
    }

    [Authorize]
    [HttpDelete("comment/{commentId}")]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest(new ErrorMesage { Message = "Logged user not found in db." });
        }

        if (!await commentService.IsOwner(commentId, user))
        {
            return Forbid();
        }
       
        if (!await commentService.DeleteComment(commentId ,user))
        {
            return BadRequest(new ErrorMesage { Message = "Failed to delete comment!!!" });
        }

        return NoContent();
    }

    [Authorize]
    [HttpPut("comment/{commentId}")]
    public async Task<IActionResult> UpdateComment(Guid commentId, [FromBody] UpdateCommentDto updateCommentDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest(new ErrorMesage { Message = "Logged user not found in db." });
        }

        if (!await commentService.UpdateCommnet(commentId, updateCommentDto, user))
        {
            return BadRequest(new ErrorMesage { Message = "Failed to update comment!!!" });
        }

        return NoContent();
    }

}