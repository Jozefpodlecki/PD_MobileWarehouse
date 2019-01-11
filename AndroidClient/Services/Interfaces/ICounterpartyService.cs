using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Common;

namespace Client.Services.Interfaces
{
    public interface ICounterpartyService
    {
        Task<HttpResult<bool>> CounterpartyExists(string name, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> NIPExists(string nip, CancellationToken token = default(CancellationToken));

        Task<HttpResult<List<Models.Counterparty>>> GetCounterparties(FilterCriteria criteria, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> AddCounterparty(Models.Counterparty model, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> UpdateCounterparty(Models.Counterparty counterparty, CancellationToken token = default(CancellationToken));
   
        Task<HttpResult<bool>> DeleteCounterparty(int id, CancellationToken token = default(CancellationToken));
    
    }
}