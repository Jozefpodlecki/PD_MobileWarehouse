using Data_Access_Layer;

namespace Common.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string userName);
    }
}