using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Dtos.User;
using server.Interfaces;
using server.Models;
using server.Utlis;
using System.Security.Claims;
using System.Web;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IEmailService emailService, IConfiguration configuration) : ControllerBase
{
    private readonly UserManager<User> userManager = userManager;
    private readonly SignInManager<User> signInManager = signInManager;
    private readonly ITokenService tokenService = tokenService;
    private readonly string frontEndResetPassword = configuration["RessetPassordUrl"]!;

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
            BadRequest(Utils.ValidationError(ModelState));
        }

        var existingUser = await userManager.FindByEmailAsync(bodyDto.Email);

        if (existingUser != null)
        {
            return BadRequest(new ErrorMesage{ Message="User already exist with this email!!!"});
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
                return Ok();
            }
                
        }

        return BadRequest(createdUser.Errors);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Unregister()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        user.Deleted = true;
        user.DeletedDate = DateTime.Now;

        await userManager.UpdateAsync(user);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(Utils.ValidationError(ModelState));
        }
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest(new ErrorMesage{
                Message="Invalid Credentials",
                Errors = [new ValidationMessage{Field=nameof(model.Email),Message="User with this email has not been found!!"}]                
            });
        }

        if (user.Deleted) { 
            return BadRequest(new ErrorMesage
            {
                Message = "Invalid Credentials",
                Errors = [new ValidationMessage { Field = nameof(model.Email), Message = "User with this email has been deleted!!" }]
            });
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded) return BadRequest(new ErrorMesage{
                Message="Invalid Credentials",
                Errors = [new ValidationMessage{Field=nameof(model.Password),Message="Wrong password!!"}]                
            });

        var (loginToken, refreshToken) = await tokenService.GenerateTokens(user);

        return Ok(new LoggedUserDataDto
        {
            Email = user.Email ?? string.Empty,
            LogInToken= loginToken,
            RefreshToken = refreshToken
        });
    }


    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await signInManager.SignOutAsync();

        return Ok();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
    {
         var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }        

        user.UserName = dto.NewName;

        var result = await userManager.UpdateAsync(user);
    
        if (!result.Succeeded) return BadRequest(new ErrorMesage{
                Message="Change user info failed!!!", 
                Details=string.Join("\n", result.Errors.Select(x => x.Description))}
            );
 
        return Ok();
    }

    [HttpPost("requestResetPassword")]
    public async Task<IActionResult> ResetPasswordRequest([FromBody]string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null) {
            return BadRequest(new ErrorMesage{Message="There is no user with this email registered!!!"});
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);

        token = HttpUtility.UrlEncode(token);

        string resetLink = $"{frontEndResetPassword}/{token}/{email}";

        await emailService.SendEmailAsync(email, "Password Reset Request", $"<h3>ChefBook</h3><p>Reset password for user <b>{user.UserName}</b>\n\nClick the following link to reset your password: <a href='{resetLink}'>Reset Password</a></p>");
        
        return Ok();
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);

        if (user == null) {
            return NotFound();
        }

        bool rightOldPassword = await userManager.CheckPasswordAsync(user, dto.OldPassword);

        if (!rightOldPassword) {
            return BadRequest(new ErrorMesage{Message = "ValidationError", Errors = [new ValidationMessage{Field=nameof(dto.OldPassword), Message="Old password is incorect"}] });
        }

        var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

        if (!result.Succeeded) {
            return BadRequest(new ErrorMesage{ Message="Password reset was not succesfull", Details=string.Join("\n", result.Errors.Select(e => e.Description))});
        }

        return Ok();
    }
}