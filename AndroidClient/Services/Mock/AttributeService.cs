using Client.Models;
using Client.Services.Interfaces;
using Client.SQLite;
using Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services.Mock
{
    public class AttributeService : BaseSQLiteService<Data_Access_Layer.Attribute>, IAttributeService
    {
        public AttributeService(SQLiteDbContext sqliteDbContext) : base(sqliteDbContext)
        {
        }

        public async Task<HttpResult<bool>> AddAttribute(Attribute model, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var result = new HttpResult<bool>();
                var entity = new Data_Access_Layer.Attribute
                {
                    Name = model.Name
                };
                await Add(entity);
                Save();

                return result;
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }

        public Task<HttpResult<bool>> DeleteAttribute(int id, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();
            Remove(id);
            Save();
            return Task.FromResult(result);
        }

        public Task<HttpResult<bool>> EditAttribute(Attribute model, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();
            var entity = new Data_Access_Layer.Attribute
            {
                Id = model.Id,
                Name = model.Name
            };
            Update(entity);
            Save();
            return Task.FromResult(result);
        }

        public Task<HttpResult<List<Attribute>>> GetAttributes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Attribute>>();
            try
            {
                result.Data = GetPaged(criteria).Select(at =>
            new Models.Attribute
            {
                Id = at.Id,
                Name = at.Name
            })
            .ToList();

                return Task.FromResult(result);
            }
            catch (System.Exception ex)
            {
                
            }
            return null;
        }
    }
}