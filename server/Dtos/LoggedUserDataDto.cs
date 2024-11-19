namespace server.Dtos;

public class LoggedUserDataDto
{
    public string Email { get; set; } = string.Empty;
    public string LogInToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}