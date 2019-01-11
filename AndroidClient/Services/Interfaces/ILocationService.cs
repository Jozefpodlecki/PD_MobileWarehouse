using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface ILocationService
    {
        Task<HttpResult<List<Models.Location>>> GetLocations(FilterCriteria criteria, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> AddLocation(Models.Location model, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> DeleteLocation(int id, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> UpdateLocation(Models.Location model, CancellationToken token = default(CancellationToken));

        Task<HttpResult<List<Models.Location>>> GetLocationsByProduct(string name, string value, CancellationToken token = default(CancellationToken));

    }
}