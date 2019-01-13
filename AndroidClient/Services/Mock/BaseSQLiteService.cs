using Client.SQLite;
using Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Services.Mock
{
    public class BaseSQLiteService<T>
        where T: class
    {
        protected readonly SQLiteDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseSQLiteService(SQLiteDbContext sqliteDbContext)
        {
            _dbContext = sqliteDbContext;
            _dbSet = _dbContext.Set<T>();
            Load();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public List<T> GetPaged(FilterCriteria criteria)
        {
            return _dbSet
                .Skip(criteria.ItemsPerPage * criteria.Page)
                .Take(criteria.ItemsPerPage)
                .ToList();
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task AddRange(ICollection<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public void Load()
        {
            _dbContext.Database.EnsureCreated();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}