using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Repository;
using Common.Repository.Interfaces;
using SQLite;

namespace Client.Repository
{
    public class NameRepository<T> : Repository<T>, INameRepository<T>
        where T : class, IName, new()
    {
        public NameRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public async Task<IEnumerable<T>> Get(string name)
        {
            return _sqliteConnection
                .GetAllWithChildren<T>(na => na.Name == name);
        }

        public async Task<IEnumerable<T>> Like(string name)
        {
            return _sqliteConnection
                .Table<T>()
                .Where(na => na.Name.Contains(name));
        }

        public async Task<T> Find(string name)
        {
            return _sqliteConnection
                .Table<T>()
                .FirstOrDefault(na => na.Name == name);
        }

        public async Task<bool> Exists(string name)
        {
            return _sqliteConnection
                .Table<T>()
                .Any(na => na.Name == name);
        }

        public new IEnumerable<T> Get(FilterCriteria criteria)
        {
            var result = _sqliteConnection
                .GetAllWithChildren<T>()
                .Skip(criteria.ItemsPerPage * criteria.Page)
                .Take(criteria.ItemsPerPage);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                result = result.Where(na => na.Name.Contains(criteria.Name));
            }

            return result;
        }
    }
}
