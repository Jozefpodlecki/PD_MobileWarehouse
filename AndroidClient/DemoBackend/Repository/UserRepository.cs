using Common;
using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public User GetByUsername(string userName)
        {
            var user = _sqliteConnection
                .GetAllWithChildren<User>(us => us.UserName == userName, true)
                .FirstOrDefault();

            var userRole = user.UserRoles.FirstOrDefault();
            var roleId = userRole.RoleId;
            userRole.Role = _sqliteConnection.Get<Role>(roleId);

            return user;
        }

        public override User Get(int id)
        {
            var user = _sqliteConnection.GetWithChildren<User>(id);

            var userRole = user.UserRoles.FirstOrDefault();
            userRole.Role = _sqliteConnection.GetWithChildren<Role>(userRole.RoleId);

            return user;
        }

        public override IEnumerable<User> Get(FilterCriteria criteria)
        {
            var users = _sqliteConnection
                .GetAllWithChildren<User>()
                .Skip(criteria.ItemsPerPage * criteria.Page)
                .Take(criteria.ItemsPerPage)
                .ToList();

            users.ForEach(us => {
                var userRole = us.UserRoles.FirstOrDefault();
                userRole.Role = _sqliteConnection.GetWithChildren<Role>(userRole.RoleId);

            });

            return users;
        }
    }
}
