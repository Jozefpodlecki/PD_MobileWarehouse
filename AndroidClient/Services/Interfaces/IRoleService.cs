using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Client.Services.Interfaces
{
    public interface IRoleService
    {
        Task<HttpResult<List<Models.Claim>>> GetClaims(CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> RoleExists(string name, CancellationToken token = default(CancellationToken));

        Task<HttpResult<List<Models.Role>>> GetRoles(FilterCriteria criteria, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> AddRole(Models.Role entity, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> UpdateRole(Models.Role entity, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> EditRole(Models.Role entity, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> DeleteRole(int id, CancellationToken token = default(CancellationToken));
    }
}