using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiServer.Controllers.Role.ViewModel;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = SiteClaimValues.Roles.Read)]
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

        [Authorize(Policy = SiteClaimValues.Roles.Read)]
        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var result = _unitOfWork.GetClaims();

            return new ObjectResult(result);
        }

        [Authorize(Policy = SiteClaimValues.Roles.Read)]
        [HttpPost("search")]
        public IActionResult GetRoles([FromBody] FilterCriteria criteria)
        {
            var result = _unitOfWork.GetRoles(criteria);

            return new ObjectResult(result);
        }

        [Authorize(Policy = SiteClaimValues.Roles.Add)]
        [HttpPut("bulk")]
        public async Task<IActionResult> AddRoles([FromBody] IEnumerable<AddRole> model)
        {
            await _unitOfWork.AddRoles(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Roles.Add)]
        [HttpPut]
        public async Task<IActionResult> AddRole([FromBody] AddRole model)
        {
            await _unitOfWork.AddRole(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Roles.Update)]
        [HttpPost]
        public async Task<IActionResult> UpdateRole([FromBody] EditRole model)
        {
            await _unitOfWork.EditRole(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Roles.Remove)]
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