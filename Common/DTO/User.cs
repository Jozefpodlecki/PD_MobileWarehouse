using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<Claim> Claims { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
