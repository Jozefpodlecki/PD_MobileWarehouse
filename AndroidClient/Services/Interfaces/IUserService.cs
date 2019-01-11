using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<HttpResult<Models.User>> GetUser(int id, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> UpdateUser(Models.User model, CancellationToken token = default(CancellationToken));

        Task<HttpResult<List<Models.User>>> GetUsers(FilterCriteria criteria, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> AddUser(Models.User user, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> DeleteUser(int id, CancellationToken token = default(CancellationToken));
    }
}