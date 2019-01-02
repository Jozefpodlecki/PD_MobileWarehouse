using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Client.Models;
using Common;
using Common.DTO;

namespace Client.Services
{
    public class RoleService : Service
    {
        public RoleService(
            ) : base("/api/role")
        {
        }

        public async Task<HttpResult<List<Models.Claim>>> GetClaims(CancellationToken token = default(CancellationToken))
        {
            return await Get<Models.Claim>("/claims", token);
        }

        public async Task<HttpResult<bool>> RoleExists(string name, CancellationToken token = default(CancellationToken))
        {
            return await Exists("name", name);
        }

        public async Task<HttpResult<List<Models.Role>>> GetRoles(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.Role>(criteria);
        }

        public async Task<HttpResult<bool>> AddRole(Models.Role entity, CancellationToken token = default(CancellationToken))
        {
            return await Put(entity, token);
        }

        public async Task<HttpResult<bool>> UpdateRole(Models.Role entity, CancellationToken token = default(CancellationToken))
        {
            return await Post(entity, token);
        }

        public async Task<HttpResult<bool>> EditRole(Models.Role entity, CancellationToken token = default(CancellationToken))
        {
            return await Post(entity, token);
        }

        public async Task<HttpResult<bool>> DeleteRole(int id, CancellationToken token = default(CancellationToken))
        {
            return await Delete(id, token);
        }
    }
}