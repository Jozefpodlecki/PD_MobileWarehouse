using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Models;
using Client.Services.Interfaces;
using Client.SQLite;
using Common;

namespace Client.Services.Mock
{
    public class RoleService : BaseSQLiteService<Models.Role>, IRoleService
    {
        public RoleService(SQLiteDbContext sqliteDbContext) : base(sqliteDbContext)
        {
        }

        public Task<HttpResult<bool>> AddRole(Role entity, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> DeleteRole(int id, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> EditRole(Role entity, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<Claim>>> GetClaims(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<Role>>> GetRoles(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> RoleExists(string name, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> UpdateRole(Role entity, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}