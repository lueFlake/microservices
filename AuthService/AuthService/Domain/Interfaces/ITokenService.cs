using Afonin.AuthService.Domain.Entities;
using System.Security.Claims;

namespace Afonin.AuthService.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}