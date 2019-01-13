using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public virtual Role Role { get; set; }
    }
}
