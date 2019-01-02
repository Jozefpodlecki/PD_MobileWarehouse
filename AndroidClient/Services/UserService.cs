using Android.App;
using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class UserService : Service
    {
        public UserService() 
            : base("/api/user")
        {

        }

        public async Task<HttpResult<Models.User>> GetUser(int id, CancellationToken token = default(CancellationToken))
        {
            return await Get<Models.User>(id, token);
        }

        public async Task<HttpResult<bool>> UpdateUser(Models.User model, CancellationToken token = default(CancellationToken))
        {
            return await Post(model, token);
        }

        public async Task<HttpResult<List<Models.User>>> GetUsers(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.User>(criteria, token);
        }

        public async Task<HttpResult<bool>> AddUser(Models.User user, CancellationToken token = default(CancellationToken))
        {
            return await Put(user, token);
        }

        public async Task<HttpResult<bool>> DeleteUser(int id, CancellationToken token = default(CancellationToken))
        {
            return await Delete(id, token);
        }
    }
}