using Client.Models;
using Client.Services.Interfaces;
using Common;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuthService : Service, IAuthService
    {
        public AuthService(HttpClientManager httpClientManager, HttpHelper httpHelper, string postFix) : base(httpClientManager, httpHelper, postFix)
        {
        }

        public async Task<HttpResult<string>> Login(Models.Login model, CancellationToken token = default(CancellationToken))
        {
            return await PostString(model,"/login", token);
        }
    }
}