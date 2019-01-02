﻿using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class CounterpartyService : Service
    {
        public CounterpartyService() : base("/api/counterparty")
        {
        }

        public async Task<HttpResult<bool>> CounterpartyExists(string name, CancellationToken token = default(CancellationToken))
        {
            return await Exists("name", name, "/counterparty");
        }

        public async Task<HttpResult<bool>> NIPExists(string nip, CancellationToken token = default(CancellationToken))
        {
            return await Exists("nip", nip, "/counterparty");
        }

        public async Task<HttpResult<List<Models.Counterparty>>> GetCounterparties(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.Counterparty>(criteria, token);
        }

        public async Task<HttpResult<bool>> AddCounterparty(Models.Counterparty model, CancellationToken token = default(CancellationToken))
        {
            return await Put(model, token);
        }

        public async Task<HttpResult<bool>> UpdateCounterparty(Models.Counterparty counterparty, CancellationToken token)
        {
            return await Post(counterparty, token);
        }

        public async Task<HttpResult<bool>> DeleteCounterparty(int id, CancellationToken token = default(CancellationToken))
        {
            return await Delete(id, token);
        }
    }
}