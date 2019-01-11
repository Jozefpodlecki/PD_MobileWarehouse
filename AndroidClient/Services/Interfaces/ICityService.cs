using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface ICityService
    {
        Task<HttpResult<List<Models.City>>> GetCities(FilterCriteria criteria, CancellationToken token = default(CancellationToken));
        
    }
}