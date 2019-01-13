using Client.Services.Interfaces;
using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AttributeService : Service, IAttributeService
    {
        public AttributeService(HttpClientManager httpClientManager, HttpHelper httpHelper, string postFix) : base(httpClientManager, httpHelper, postFix)
        {
        }

        public async Task<HttpResult<List<Models.Attribute>>> GetAttributes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.Attribute>(criteria, token);
        }

        public async Task<HttpResult<bool>> AddAttribute(Models.Attribute model, CancellationToken token = default(CancellationToken))
        {
            return await Put(model, token);
        }

        public async Task<HttpResult<bool>> EditAttribute(Models.Attribute model, CancellationToken token = default(CancellationToken))
        {
            return await Put(model, token);
        }

        public async Task<HttpResult<bool>> DeleteAttribute(int id, CancellationToken token = default(CancellationToken))
        {
            return await Delete(id, token);
        }
    }
}