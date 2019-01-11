using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<HttpResult<string>> Login(Models.Login model, CancellationToken token = default(CancellationToken));
    }
}