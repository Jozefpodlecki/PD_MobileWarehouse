using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class NameRepository<T> : Repository<T>, INameRepository<T>
        where T : class, IName, new()
    {
        public NameRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<T>> Get(string name)
        {
            return await _dbset
                .Where(it => it.Name == name)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> Like(string name)
        {
            return await _dbset
                .Where(it => it.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<T> Find(string name)
        {
            return await _dbset
                .FirstOrDefaultAsync(it => it.Name == name);
        }

        public async Task<bool> Exists(string name)
        {
            return await _dbset
                .AnyAsync(it => it.Name == name);
        }

        public new IEnumerable<T> Get(FilterCriteria criteria)
        {
            var result = _dbset
                .Skip(criteria.ItemsPerPage * criteria.Page)
                .Take(criteria.ItemsPerPage);

            if (string.IsNullOrEmpty(criteria.Name))
            {
                result = result.Where(na => na.Name.Contains(criteria.Name));
            }

            return result;
        }
    }
}
