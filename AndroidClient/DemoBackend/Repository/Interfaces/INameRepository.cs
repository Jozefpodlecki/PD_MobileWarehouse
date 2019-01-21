using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;

namespace Common.Repository.Interfaces
{
    public interface INameRepository<T> : IRepository<T>
        where T : class, IName
    {
        Task<IEnumerable<T>> Get(string name);
        Task<IEnumerable<T>> Like(string name);
        Task<T> Find(string name);
        Task<bool> Exists(string name);
    }
}
