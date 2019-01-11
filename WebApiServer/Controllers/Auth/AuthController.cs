using Microsoft.AspNetCore.Mvc;
using WebApiServer.Controllers.Auth.ViewModel;
using WebApiServer.Providers;
using System.Threading.Tasks;
using WebApiServer.Managers;
using Common;
using Microsoft.AspNetCore.Authorization;

namespace WebApiServer.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly PasswordManager _passwordManager;

        public AuthController(
            UnitOfWork unitOfWork,
            JwtTokenProvider jwtTokenProvider,
            PasswordManager passwordManager
            )
        {
            _unitOfWork = unitOfWork;
            _jwtTokenProvider = jwtTokenProvider;
            _passwordManager = passwordManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Login model)
        {
            var user = _unitOfWork.GetUser(model.Username);

            if (user == null)
            {
                ModelState.AddModelError("Login",Errors.USER_NOT_FOUND);

                return BadRequest(ModelState);
            }

            if (!_passwordManager.Compare(user,model.Password))
            {
                ModelState.AddModelError("Password", Errors.INVALID_PASSWORD);

                return BadRequest(ModelState);
            };

            var claims = await _unitOfWork.GetUserClaims(user);

            await _unitOfWork.UpdateLastLogin(user);

            var token = _jwtTokenProvider.CreateToken(user, claims);

            return new ObjectResult(token);
        }
    }
}
