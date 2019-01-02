using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public enum UserStatus : byte
    {
        ACTIVE,
        BLOCKED,
        DELETED
    }

    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public UserStatus UserStatus { get; set; }

        public virtual new byte[] PasswordHash { get; set; }

        public DateTime LastLogin { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
    }
}
