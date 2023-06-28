using System.Collections.Generic;
using System.Threading.Tasks;
using Afonin.AuthService.Domain.Entities;
using Afonin.AuthService.Domain.Models;

namespace Afonin.AuthService.Domain.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(RegisterRequest userModel);
        IEnumerable<UserResponse> GetAll();
        User GetById(int id);
        AuthenticateResponse UpdateToken(TokensRequest model);
    }
}