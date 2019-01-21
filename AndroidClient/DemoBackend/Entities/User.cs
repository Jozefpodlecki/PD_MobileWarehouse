using SQLite;
using SQLiteNetExtensions.Attributes;
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

    public class User : BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool TwoFactorEnabled { get; set; }
     
        public bool PhoneNumberConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string SecurityStamp { get; set; }

        public bool EmailConfirmed { get; set; }

        public string NormalizedEmail { get; set; }

        [NotNull]
        public string Email { get; set; }

        public string NormalizedUserName { get; set; }

        [NotNull, Unique]
        public string UserName { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public UserStatus UserStatus { get; set; }

        [NotNull]
        public byte[] PasswordHash { get; set; }

        public DateTime? LastLogin { get; set; }

        [OneToMany]
        public List<UserRole> UserRoles { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<UserClaim> UserClaims { get; set; }

        public Role Role => UserRoles?.Select(ur => ur.Role).FirstOrDefault();
    }
}
