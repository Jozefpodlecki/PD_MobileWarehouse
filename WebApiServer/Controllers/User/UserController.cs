using Common;
using Common.DTO;
using Common.IUnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiServer.Controllers.User.ViewModel;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;

        public UserController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = SiteClaimValues.Users.Read)]
        [HttpHead]
        public IActionResult UserExists([FromQuery] UserExists model)
        {
            var result = _unitOfWork.UserExists(model);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Policy = SiteClaimValues.Users.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _unitOfWork.GetUser(id);

            return new ObjectResult(result);
        }

        [Authorize(Policy = SiteClaimValues.Users.Read)]
        [HttpPost("search")]
        public IActionResult GetUsers(FilterCriteria criteria)
        {
            var result = _unitOfWork.GetUsers(criteria);

            return new ObjectResult(result);
        }

        [Authorize(Policy = SiteClaimValues.Users.Update)]
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] EditUser model)
        {
            await _unitOfWork.EditUser(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Users.Add)]
        [HttpPut]
        public async Task<IActionResult> AddUser([FromBody] AddUser model)
        {
            await _unitOfWork.AddUser(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Users.Add)]
        [HttpPut("bulk")]
        public async Task<IActionResult> AddUsers([FromBody] IEnumerable<AddUser> users)
        {
            await _unitOfWork.AddUsers(users);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Users.Remove)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.GetUser(id);

            if(user == null)
            {
                ModelState.AddModelError("id", Errors.USER_NOT_FOUND);

                return BadRequest(ModelState);
            }

            await _unitOfWork.DeleteUser(id);

            return Ok();
        }
    }
}
