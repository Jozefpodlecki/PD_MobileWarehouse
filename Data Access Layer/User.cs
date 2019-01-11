using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_Access_Layer
{
    public enum UserStatus : byte
    {
        ACTIVE,
        BLOCKED,
        DELETED
    }

    public class User : IdentityUser<int>, IBaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public UserStatus UserStatus { get; set; }

        public virtual new byte[] PasswordHash { get; set; }

        public DateTime? LastLogin { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }

        public string Role => UserRoles.Select(ur => ur.Role.Name).FirstOrDefault();

        public DateTime? CreatedAt { get; set; }
        public User CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public User LastModifiedBy { get; set; }
        public int? LastModifiedById { get; set; }
    }
}
