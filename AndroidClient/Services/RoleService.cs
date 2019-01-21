using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Client.Services.Interfaces;
using Common;

namespace Client.Services
{
    public class RoleService : Service, IRoleService
    {
        public RoleService(HttpClientAuthorizationManager httpClientManager, HttpHelper httpHelper, string postFix) : base(httpClientManager, httpHelper, postFix)
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