using Client.Models;
using Client.Services.Interfaces;
using Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class LocationService : Service, ILocationService
    {
        public LocationService(
         ) : base("/api/location")
        {
        }

        public async Task<HttpResult<bool>> LocationExists(string name, CancellationToken token = default(CancellationToken))
        {
            return await Exists("name", name);
        }

        public async Task<HttpResult<List<Models.Location>>> GetLocations(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.Location>(criteria,null, token);
        }

        public async Task<HttpResult<bool>> AddLocation(Models.Location model, CancellationToken token = default(CancellationToken))
        {
            return await Put(model, token);
        }

        public async Task<HttpResult<bool>> DeleteLocation(int id, CancellationToken token = default(CancellationToken))
        {
            return await Delete(id, token);
        }

        public async Task<HttpResult<bool>> UpdateLocation(Models.Location model, CancellationToken token = default(CancellationToken))
        {
            return await Post(model, token);
        }

        public async Task<HttpResult<List<Models.Location>>> GetLocationsByProduct(string name, string value, CancellationToken token = default(CancellationToken))
        {
            return await Get<Models.Location>(name, value, "product", token);
        }
    }
}