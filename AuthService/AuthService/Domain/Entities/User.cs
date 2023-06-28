using Afonin.AuthService.Domain.Base;
using Microsoft.AspNetCore.Routing.Matching;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Afonin.AuthService.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Index(IsUnique = true)]
        [Required]
        public string Username { get; set; }
        [EmailAddress]
        [Required]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public Session? Session { get; set; }
    }
}