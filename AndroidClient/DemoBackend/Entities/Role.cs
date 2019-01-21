using Common;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Role : BaseEntity, IName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, Unique, MaxLength(50)]
        public string Name { get; set; }

        public string NormalizedName => Name.ToUpper();

        public string ConcurrencyStamp { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<UserRole> UserRoles { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<RoleClaim> RoleClaims { get; set; }
    }
    
}
