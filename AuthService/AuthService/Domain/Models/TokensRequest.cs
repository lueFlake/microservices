namespace Afonin.AuthService.Domain.Models
{
    public class TokensRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
