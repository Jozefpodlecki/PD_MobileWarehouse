using Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public class Role : IdentityRole<int>, IName, IBaseEntity
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

        public DateTime CreatedAt { get; set; }
        public User CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        public DateTime LastModifiedAt { get; set; }
        public User LastModifiedBy { get; set; }
        public int? LastModifiedById { get; set; }
    }
    
}
