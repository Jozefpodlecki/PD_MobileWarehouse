using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data_Access_Layer.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public User GetByUsername(string userName)
        {
            return _dbset.FirstOrDefault(us => us.UserName == userName);
        }
    }
}
