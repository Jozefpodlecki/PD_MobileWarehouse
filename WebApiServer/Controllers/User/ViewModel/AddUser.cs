using Common.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.User.ViewModel
{
    public class AddUser
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Claim> Claims { get;set; }

        public Common.DTO.Role Role { get; set; }
    }
}
