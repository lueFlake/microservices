using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afonin.AuthService.Domain.Models
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
