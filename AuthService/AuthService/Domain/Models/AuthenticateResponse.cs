using Afonin.AuthService.Domain.Entities;

namespace Afonin.AuthService.Domain.Models
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string accessToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            AccessToken = accessToken;
            RefreshToken = user.Session.RefreshToken;
        }
    }
}