using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data_Access_Layer.Repository
{
    public class UserClaimRepository : Repository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<UserClaim> GetForUser(User user)
        {
            return _dbset
                .Where(uc => uc.UserId == user.Id)
                .ToList();
        }
    }
}
