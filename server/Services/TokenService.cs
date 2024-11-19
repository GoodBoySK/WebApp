using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using server.Interfaces;
using server.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace server.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly UserManager<User> _userManager;
    private readonly SymmetricSecurityKey _key;


    private const int RefreshTokenSize = 32;
    private const string TokenName = "RefreshToken";
    

        public TokenService(IConfiguration config , UserManager<User> userManager)
    {
        _config = config;
        _userManager = userManager;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
    }

    private string GenerateLogInToken(User user)
    {
        if (user.Email is null)
        {
            throw new ArgumentNullException(nameof(user.Email));
        }

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.NameId, user.Id)
        ];

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptior);

        return tokenHandler.WriteToken(token);
    }

    private async Task<string> GenerateRefreshToken(User user)
    {
        var token = GenerateRandomToken();

        await _userManager.SetAuthenticationTokenAsync(user,"Default" ,TokenName, token);

        return token;
    }

    private static string GenerateRandomToken()
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(RefreshTokenSize); ; // 32 bytes = 256 bits
        return Convert.ToBase64String(tokenBytes);
    }

    public async Task RevokeRefreshToken(User user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user, "Default", TokenName);
    }

    public async Task<(string accessToken, string refreshToken)> GenerateTokens(User user)
    {
        var newAccessToken = GenerateLogInToken(user);
        //var newRefreshToken = await GenerateRefreshToken(user);

        return (newAccessToken, "");
    }

    public async Task<(string accessToken, string newRefreshToken)> RefreshTokens(User user, string refreshToken)
    {
        var storedToken = await _userManager.GetAuthenticationTokenAsync(user, "Default", TokenName);

        // Validate the refresh token
        if (storedToken != refreshToken)
        {
            throw new SecurityTokenException("Invalid refresh token.");
        }

        // Generate new tokens
        var newAccessToken = GenerateLogInToken(user);
        var newRefreshToken = await GenerateRefreshToken(user);

        return (newAccessToken, newRefreshToken);
    }

    
}