using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using Common.Managers;
using Common.Providers;
using WebApiServer;

namespace Client.Services.Mock
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IPasswordManager _passwordManager;

        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtTokenProvider jwtTokenProvider,
            IPasswordManager passwordManager) : base(unitOfWork)
        {
            _passwordManager = passwordManager;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<HttpResult<string>> Login(Login model, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<string>();

            var user = _unitOfWork.GetUser(model.Username);
            
            if(user == null)
            {
                result.Error = new System.Collections.Generic.Dictionary<string, string[]>
                {
                    { "Username", new []{ "Username not found" } }
                };

                return result;
            }

            if (!_passwordManager.Compare(user, model.Password))
            {
                result.Error = new System.Collections.Generic.Dictionary<string, string[]>
                {
                    { "Password", new []{ "Invalid password" } }
                };
            };

            var claims = await _unitOfWork.GetUserClaims(user);

            await _unitOfWork.UpdateLastLogin(user);

            result.Data = _jwtTokenProvider.CreateToken(user, claims);

            return await Task.FromResult(result);
        }
    }
}