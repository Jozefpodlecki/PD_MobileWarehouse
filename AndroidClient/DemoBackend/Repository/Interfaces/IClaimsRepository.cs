using Common.DTO;
using System.Collections.Generic;

namespace Common.Repository.Interfaces
{
    public interface IClaimsRepository
    {
        List<Claim> GetClaims();
    }
}