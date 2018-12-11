
using Android.App;
using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class LocationService : Service
    {
        public LocationService(
         Activity activity
         ) : base(activity, "api/location")
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

        public async Task<HttpResult<bool>> AddLocation(Models.Location model)
        {
            return await Put(model);
        }

        public async Task<HttpResult<bool>> DeleteLocation(int id)
        {
            return await Delete(id);
        }
    }
}