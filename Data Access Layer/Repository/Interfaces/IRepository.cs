﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Repository.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> Entities { get; }
        Task<T> Get(int id);
        Task<IEnumerable<T>> Get();
        IEnumerable<T> Get(FilterCriteria criteria);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
