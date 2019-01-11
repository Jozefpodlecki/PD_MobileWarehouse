using System.Collections.Generic;

namespace Common.DTO
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public List<Claim> Claims { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
