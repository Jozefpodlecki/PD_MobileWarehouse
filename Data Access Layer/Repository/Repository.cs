using Common;
using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbset;

        public IEnumerable<T> Entities => _dbset;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = dbContext.Set<T>();
        }

        public async Task<T> Get(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _dbset.ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(entities);
        }

        public IEnumerable<T> Get(FilterCriteria criteria)
        {
            return _dbset
                .Skip(criteria.ItemsPerPage * criteria.Page)
                .Take(criteria.ItemsPerPage);
        }
    }
}