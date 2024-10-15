using server.Models;

namespace server.Interfaces;


public interface ITokenService
{
    public string CreateToken(User user);
}
