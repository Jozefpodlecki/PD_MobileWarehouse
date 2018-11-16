using Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> Entities { get; }
        Task<T> Get(int id);
        Task<IEnumerable<T>> Get();
        Task<EntityEntry<T>> Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> Save();
    }
}
