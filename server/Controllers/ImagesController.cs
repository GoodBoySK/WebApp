using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Services;
using System.Security.Claims;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController(IImageStorageService imageStorageService, UserManager<User> userManager) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        try
        {
            var (image , mediaFile) = await imageStorageService.RetriveImageAsync(id);

            if (mediaFile == null)
            {
                return NotFound("MEDIANOTFOUND");
            }

            return File(image, GetContentType(Path.GetExtension(mediaFile.Path)).TrimStart('.'));
        }
        catch (FileNotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImageImage([FromForm] IFormFile? file)
    {

        if (file == null)
        {
            //return BadRequest(file);
        }

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

        var mediaFile = await imageStorageService.StoreImageAsync(file, user);

        return Ok(mediaFile.Id);
    }
        
    private static string GetContentType(string extension)
    {
        return extension switch
        {
            "jpg" => "image/jpeg",
            "jpeg" => "image/jpeg",
            "png" => "image/png",
            "gif" => "image/gif",
            _ => "application/octet-stream"
        };
    }
}