using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Client.SQLite;
using Common;

namespace Client.Services.Mock
{
    public class CityService : BaseSQLiteService<Models.City>, ICityService
    {
        public CityService(SQLiteDbContext sqliteDbContext) : base(sqliteDbContext)
        {
        }

        public async Task<HttpResult<List<City>>> GetCities(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return new HttpResult<List<City>>();
        }
    }
}