using AdAstra.HRPlatform.API.Services.Users;
using System.Runtime.CompilerServices;
using Afonin.AuthService.Domain.Interfaces;

namespace Afonin.AuthService.Services.Injection
{
    public static class ServicesInjection
    {
        public static void AddMainServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHashingService, PasswordHashingService>();
        }
    }
}
