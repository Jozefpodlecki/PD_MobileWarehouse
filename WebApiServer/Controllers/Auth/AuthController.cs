using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiServer.Controllers.Auth.ViewModel;
using System.Linq;
using WebApiServer.Providers;
using WebApiServer.Constants;
using System.Threading.Tasks;
using WebApiServer.Managers;
using Common;
using Microsoft.Extensions.Options;

namespace WebApiServer.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<Data_Access_Layer.User> _userManager;
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly PasswordManager _passwordManager;

        public AuthController(
            UserManager<Data_Access_Layer.User> userManager,
            PasswordManager passwordManager,
            UnitOfWork unitOfWork,
            IOptions<JwtConfiguration> jwtConfiguration,
            RoleManager<Data_Access_Layer.Role> roleManager
            )
        {
            _jwtTokenProvider = new JwtTokenProvider(jwtConfiguration, userManager, roleManager);
            _userManager = userManager;
            _passwordManager = passwordManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Login model)
        {
            var user = _userManager
                .Users
                .FirstOrDefault(us => us.UserName == model.Username);

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

            var token = await _jwtTokenProvider.CreateToken(user);

            return new ObjectResult(token);
        }
    }
}
