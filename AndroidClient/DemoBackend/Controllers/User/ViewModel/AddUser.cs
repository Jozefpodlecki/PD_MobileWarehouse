using Common.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.User.ViewModel
{
    public class AddUser
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Claim> Claims { get;set; }

        public Common.DTO.Role Role { get; set; }
    }
}
