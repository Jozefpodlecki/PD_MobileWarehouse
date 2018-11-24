using Common;
using Common.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiServer.Constants;
using WebApiServer.Controllers.User.ViewModel;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UnitOfWork _unitOfWork;

        public UserController(
            UnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        [HttpHead]
        public IActionResult EmailExists([FromQuery] string username,[FromQuery] string email)
        {
            var result = _unitOfWork
                .UserRepository
                .Entities
                .Any(co => co.Email == email || co.UserName == username);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost("search")]
        public IActionResult GetUsers(FilterCriteria criteria)
        {
            var query = _unitOfWork
                .UserRepository
                .Entities
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(pt => pt.UserName.Contains(criteria.Name));
            }

            var result = query
                .OrderBy(pr => pr.UserName)
                .Select(pr => new Common.DTO.User
                {
                    Id = pr.Id,
                    Username = pr.UserName,
                    Email = pr.Email,
                    Role = pr.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault(),
                    Claims = pr.UserClaims
                        .Select(cl =>
                        new Claim {
                            Type = cl.ClaimType,
                            Value = cl.ClaimValue
                        }).ToList()
                })
                .ToList();

            return new ObjectResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var result = _unitOfWork
                .UserRepository
                .Entities
                .FirstOrDefault(usr => usr.Id == id);

            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] EditUser model)
        {
            var user = new Common.DTO.User
            {
                Id = model.Id,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Claims = model.Claims,
                Role = model.Role
            };

            await _unitOfWork.EditUser(user);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddUser([FromBody] AddUser model)
        {
            var user = new Common.DTO.User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Claims = model.Claims,
                Role = model.Role
            };

            await _unitOfWork.AddUser(user);

            return Ok();
        }

        [HttpPut("mass")]
        public async Task<IActionResult> AddUser([FromBody] IEnumerable<AddUser> users)
        {
            foreach (var user in users)
            {
                var entity = new Common.DTO.User
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    Claims = user.Claims,
                    Role = user.Role
                };

                await _unitOfWork.AddUser(entity);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.Get(id);

            if(user == null)
            {
                ModelState.AddModelError("id", Errors.USER_NOT_FOUND);

                return BadRequest(ModelState);
            }

            _unitOfWork.DeleteUser(user);

            return Ok();
        }
    }
}
