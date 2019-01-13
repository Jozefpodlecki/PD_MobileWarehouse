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
    public class CounterpartyService : BaseSQLiteService<Models.Counterparty>, ICounterpartyService
    {
        public CounterpartyService(SQLiteDbContext sqliteDbContext) : base(sqliteDbContext)
        {
        }

        public Task<HttpResult<bool>> AddCounterparty(Counterparty model, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> CounterpartyExists(string name, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> DeleteCounterparty(int id, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<Counterparty>>> GetCounterparties(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> NIPExists(string nip, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> UpdateCounterparty(Counterparty counterparty, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}