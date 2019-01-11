using Client.Services.Interfaces;
using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class CityService : Service, ICityService
    {
        public CityService() : base("/api/city")
        {
        }

        public async Task<HttpResult<List<Models.City>>> GetCities(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.City>(criteria, token);
        }
    }
}