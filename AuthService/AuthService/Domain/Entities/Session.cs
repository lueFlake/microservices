using Afonin.AuthService.Domain.Base;

namespace Afonin.AuthService.Domain.Entities
{
    public class Session : BaseEntity
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}

