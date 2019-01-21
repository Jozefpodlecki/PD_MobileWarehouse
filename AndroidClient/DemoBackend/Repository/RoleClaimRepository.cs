using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using System.Collections.Generic;

namespace Client.Repository
{
    public class RoleClaimRepository : Repository<RoleClaim>, IRoleClaimRepository
    {
        public RoleClaimRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public List<RoleClaim> GetForRole(int roleId)
        {
            return _sqliteConnection
                .Table<RoleClaim>()
                .Where(rc => rc.RoleId == roleId)
                .ToList();
        }
    }
}
