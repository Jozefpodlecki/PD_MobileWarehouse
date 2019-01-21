using System.Collections.Generic;
using System.Linq;
using Common;
using Common.DTO;
using Common.Repository.Interfaces;

namespace Common.Repository
{
    public class ClaimsRepository : IClaimsRepository
    {
        public List<Claim> GetClaims()
        {
            return SiteClaimValues
                .ClaimValues
                .Select(cv => new Claim
                {
                    Type = SiteClaimTypes.Permission,
                    Value = cv
                })
                .ToList();
        }
    }
}