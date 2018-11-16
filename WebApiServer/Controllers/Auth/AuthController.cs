using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiServer.Controllers.Auth.ViewModel;
using System.Linq;
using WebApiServer.Providers;
using WebApiServer.Constants;
using System.Threading.Tasks;
using WebApiServer.Managers;
using Common;

namespace WebApiServer.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<Data_Access_Layer.User> _signInManager;
        private readonly UserManager<Data_Access_Layer.User> _userManager;
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly PasswordManager _passwordManager;
        public readonly UnitOfWork _unitOfWork;

        public AuthController(
            SignInManager<Data_Access_Layer.User> signInManager,
            UserManager<Data_Access_Layer.User> userManager,
            JwtTokenProvider jwtTokenProvider,
            PasswordManager passwordManager,
            UnitOfWork unitOfWork
            )
        {
            _jwtTokenProvider = jwtTokenProvider;
            _signInManager = signInManager;
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

        [HttpPost]
        public IActionResult Register([FromBody]Register model)
        {
            return null;
        }
    }
}
