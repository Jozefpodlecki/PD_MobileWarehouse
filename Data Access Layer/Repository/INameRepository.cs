using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public interface INameRepository<T> : IRepository<T>
        where T : class, IName
    {
        Task<IEnumerable<T>> Get(string name);
        Task<IEnumerable<T>> Like(string name);
        Task<T> Find(string name);

    }
}
