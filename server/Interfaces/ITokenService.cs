using server.Models;

namespace server.Interfaces;


public interface ITokenService
{
    Task RevokeRefreshToken(User user);
    Task<(string accessToken, string refreshToken)> GenerateTokens(User user);
    Task<(string accessToken, string newRefreshToken)> RefreshTokens(User user, string refreshToken);
}
