
using System.ComponentModel.DataAnnotations;

namespace server.Dtos;

public class RegisterDto
{
    [Required]
    public required string UserName { get; set; }
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}