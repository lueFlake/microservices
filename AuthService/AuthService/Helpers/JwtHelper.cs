using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Afonin.AuthService.Helpers
{

    public static class JwtHelper
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));
        }
    }
}