using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Client.SQLite;
using Common;

namespace Client.Services.Mock
{
    public class UserService : BaseSQLiteService<Models.User>, IUserService
    {
        public UserService(SQLiteDbContext sqliteDbContext) : base(sqliteDbContext)
        {
        }

        public Task<HttpResult<bool>> AddUser(User user, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> DeleteUser(int id, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<User>> GetUser(int id, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<User>>> GetUsers(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> UpdateUser(User model, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}