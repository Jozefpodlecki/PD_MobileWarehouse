using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;

namespace Client.Services.Mock
{
    public class AuthService : IAuthService
    {
        public Task<HttpResult<string>> Login(Login model, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<string>();

            return Task.FromResult(result);
        }
    }
}