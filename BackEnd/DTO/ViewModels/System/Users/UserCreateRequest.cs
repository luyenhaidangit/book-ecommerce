using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels.System.Users
{
    public class UserCreateRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Numberphone { get; set; }

        public string Role { get; set; }
    }
}
