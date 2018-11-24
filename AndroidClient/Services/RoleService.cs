using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Common;
using Common.DTO;

namespace Client.Services
{
    public class RoleService : Service
    {
        public RoleService(
            Activity activity
            ) : base(activity, "api/role")
        {
        }

        public async Task<HttpResult<List<Claim>>> GetClaims()
        {
            return await Get<Claim>("/claims");
        }

        public async Task<HttpResult<bool>> RoleExists(string name)
        {
            return await Exists("name", name);
        }

        public async Task<HttpResult<List<Role>>> GetRoles(FilterCriteria criteria)
        {
            return await PostPaged<Role>(criteria);
        }

        public async Task<HttpResult<bool>> AddRole(Role role)
        {
            return await Put(role);
        }

        public async Task<HttpResult<bool>> EditRole(Role role)
        {
            return await Post(role);
        }

        public async Task<HttpResult<bool>> DeleteRole(int id)
        {
            return await Delete(id);
        }
    }
}