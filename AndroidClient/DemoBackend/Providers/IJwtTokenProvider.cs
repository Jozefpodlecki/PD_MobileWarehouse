
using Data_Access_Layer;
using System.Collections.Generic;
using System.Security.Claims;

namespace Common.Providers
{
    public interface IJwtTokenProvider
    {
        string CreateToken(User user, IList<Claim> permissionClaims);
    }
}
