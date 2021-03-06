﻿using Client.Services.Interfaces;
using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class UserService : Service, IUserService
    {
        public UserService(HttpClientAuthorizationManager httpClientManager, HttpHelper httpHelper, string postFix) : base(httpClientManager, httpHelper, postFix)
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