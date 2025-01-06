using System.ComponentModel.DataAnnotations;

namespace server.Dtos.User;

public class RegisterDto
{
    [Required]
    [MaxLength(100, ErrorMessage = "Username cant be longer than 100 characters")]
    [MinLength(3, ErrorMessage = "UserName must be at least 3 characters")]
    public required string UserName { get; set; }
    [Required]
    [MinLength(5, ErrorMessage = "Password must be at least 5 cahracters long")]
    public string Password { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}