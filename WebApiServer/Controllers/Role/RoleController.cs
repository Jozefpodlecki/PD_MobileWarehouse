using Common;
using Data_Access_Layer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiServer.Constants;
using WebApiServer.Controllers.Role.ViewModel;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RoleController(
            UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpHead]
        public IActionResult RoleExists([FromQuery] RoleExists model)
        {
            var result = _unitOfWork.RoleExists(model);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var result = _unitOfWork.GetClaims();

            return new ObjectResult(result);
        }

        [HttpPost("search")]
        public IActionResult GetRoles([FromBody] FilterCriteria criteria)
        {
            var result = _unitOfWork.GetRoles(criteria);

            return new ObjectResult(result);
        }

        [HttpPut("bulk")]
        public async Task<IActionResult> AddRoles([FromBody] IEnumerable<AddRole> model)
        {
            await _unitOfWork.AddRoles(model);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddRole([FromBody] AddRole model)
        {
            await _unitOfWork.AddRole(model);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole([FromBody] EditRole model)
        {
            await _unitOfWork.EditRole(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _unitOfWork.GetRole(id);

            if (role.UserRoles.Any())
            {
                ModelState.AddModelError("UserRoles", Errors.USERS_ASSIGNED_TO_ROLE);

                return BadRequest(ModelState);
            }

            if (role == null)
            {
                ModelState.AddModelError("id", Errors.ROLE_NOT_FOUND);

                return BadRequest(ModelState);
            }

            _unitOfWork.DeleteRole(role);

            return Ok();
        }
    }
}