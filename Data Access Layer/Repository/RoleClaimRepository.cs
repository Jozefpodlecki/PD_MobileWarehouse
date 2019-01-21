using System;
using System.Collections.Generic;
using System.Linq;
using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class RoleClaimRepository : Repository<RoleClaim>, IRoleClaimRepository
    {
        public RoleClaimRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<RoleClaim> GetForRole(Role role)
        {
            return _dbset
                .Where(rc => rc.RoleId == role.Id)
                .ToList();
        }
    }
}
