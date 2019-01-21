using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Client.Repository
{
    public class UserClaimRepository : Repository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public List<UserClaim> GetForUser(User user)
        {
            return _sqliteConnection
                .Table<UserClaim>()
                .Where(uc => uc.UserId == user.Id)
                .ToList();
        }
    }
}
