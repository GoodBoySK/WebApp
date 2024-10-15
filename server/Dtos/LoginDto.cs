using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace server.Dtos;

public class LoginDto
{
    [Required]
    [DefaultValue("user@example.com")]
    public string Email { get; set; } = string.Empty;
    [Required]
    [DefaultValue("P@$$w0rd")]
    public string Password { get; set; } = string.Empty;
}