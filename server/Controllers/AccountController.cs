using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Interfaces;
using server.Models;
using System.Security.Claims;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService) : ControllerBase
{
    [Authorize]
    [HttpGet("loggedUser")]
    public async Task<IActionResult> GetLoggedUser()
    {
        var user = await userManager.GetUserAsync(User);

        return Ok(user);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterDto bodyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        {
            return BadRequest("User already exist with this email!!!");
        }

        var user = new User
        {
            UserName = bodyDto.UserName,
            Email = bodyDto.Email
        };

        var createdUser = await userManager.CreateAsync(user, bodyDto.Password);

        if (createdUser.Succeeded)
        {
            //var role = await userManager.AddToRoleAsync(user, model.Role);
            if (true)
            {
                await userManager.UpdateAsync(user);
                return Ok("User Created Successfully");
            }
                
        }

        return BadRequest(createdUser.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Unauthorized("Invalid Email");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded) return Unauthorized("Invalid Password");

        return Ok(new LoggedUserDataDto
        {
            Email = user.Email ?? string.Empty,
            Token = tokenService.CreateToken(user)
        });
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Unregister()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var result = await userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest("Failed to delete user account");
    }

}