using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
