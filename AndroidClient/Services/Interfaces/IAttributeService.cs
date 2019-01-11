using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface IAttributeService
    {
        Task<HttpResult<List<Models.Attribute>>> GetAttributes(FilterCriteria criteria, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> AddAttribute(Models.Attribute model, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> EditAttribute(Models.Attribute model, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> DeleteAttribute(int id, CancellationToken token = default(CancellationToken));
    }
}