using Data_Access_Layer;
using System.Collections.Generic;

namespace Common.Repository.Interfaces
{
    public interface IUserClaimRepository : IRepository<UserClaim>
    {
        List<UserClaim> GetForUser(User user);
    }
}