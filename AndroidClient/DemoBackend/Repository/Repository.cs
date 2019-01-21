using Common;
using Common.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        protected readonly ISQLiteConnection _sqliteConnection;

        public IEnumerable<T> Entities => _sqliteConnection.Table<T>();
        public IEnumerable<T> EntitiesWithChildren => _sqliteConnection.GetAllWithChildren<T>(null, true);

        public Repository(ISQLiteConnection sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
        }

        public virtual async Task<T> Get(int id)
        {
            return _sqliteConnection.GetWithChildren<T>(id);
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return _sqliteConnection.Table<T>().ToList();
        }

        public virtual async Task Add(T entity)
        {
            _sqliteConnection.Insert(entity);
        }

        public virtual async Task AddRange(IEnumerable<T> entities)
        {
            _sqliteConnection.InsertAll(entities);
        }

        public virtual void Update(T entity)
        {
            _sqliteConnection.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            _sqliteConnection.Delete(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _sqliteConnection.Delete(entity);
            }
        }

        public virtual IEnumerable<T> Get(FilterCriteria criteria)
        {
            return _sqliteConnection
                .Table<T>()
                .Skip(criteria.ItemsPerPage * criteria.Page)
                .Take(criteria.ItemsPerPage);
        }

        public virtual bool Any()
        {
            return _sqliteConnection
                .Table<T>()
                .Any();
        }
    }
}