using Client.Models;
using Client.Services.Interfaces;
using Common;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuthService : Service, IAuthService
    {
        public AuthService(
            ) : base("/api/auth")
        {
        }
        
        public async Task<HttpResult<string>> Login(Models.Login model, CancellationToken token = default(CancellationToken))
        {
            return await PostString(model,"/login", token);
        }
    }
}