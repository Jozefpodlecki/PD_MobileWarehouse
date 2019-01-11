using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Common.Services
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResolverService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CanUserSeeDetails()
        {
            var httpContext = _httpContextAccessor
                .HttpContext;

            if (httpContext == null)
            {
                return false;
            }

            var seeDetailsClaim = httpContext
                .User
                .FindFirst(cl => cl.Value == PolicyTypes.SeeDetails);

            if(seeDetailsClaim == null)
            {
                return false;
            }

            return true;
        }

        public int? GetUserId()
        {
            var httpContext = _httpContextAccessor
                .HttpContext;

            if(httpContext == null)
            {
                return null;
            }

            var userIdClaim = httpContext
                .User
                .FindFirst(ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null)
            {
                return null;
            }
            
            return int.Parse(userIdClaim.Value);
        }
    }
}
