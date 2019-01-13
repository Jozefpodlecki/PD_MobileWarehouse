using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public class UserClaim : IdentityUserClaim<int> 
    {
        public virtual User User { get; set; }
    }
}
