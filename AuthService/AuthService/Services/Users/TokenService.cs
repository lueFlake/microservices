using Afonin.AuthService.Domain.Entities;
using Afonin.AuthService.Domain.Interfaces;
using Afonin.AuthService.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AdAstra.HRPlatform.API.Services.Users
{
    public class TokenService : ITokenService
    {
        public readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
            var jwt = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                signingCredentials: new SigningCredentials(
                    JwtHelper.GetSymmetricSecurityKey(_configuration["JwtSettings:Secret"]),
                    SecurityAlgorithms.Aes256CbcHmacSha512)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JwtHelper.GetSymmetricSecurityKey(_configuration["JwtSettings:Secret"]),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.Aes256CbcHmacSha512, 
                                                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
    }
}
