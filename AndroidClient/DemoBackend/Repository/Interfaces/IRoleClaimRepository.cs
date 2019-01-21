using System.Collections.Generic;
using Data_Access_Layer;

namespace Common.Repository.Interfaces
{
    public interface IRoleClaimRepository : IRepository<RoleClaim>
    {
        List<RoleClaim> GetForRole(int roleId);
    }
}