using Data_Access_Layer;
using System.Collections.Generic;

namespace Common.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string userName);
        List<User> GetByRole(int id);
    }
}