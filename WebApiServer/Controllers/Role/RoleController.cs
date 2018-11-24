using Common;
using Data_Access_Layer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult RoleExists([FromQuery] string name)
        {
            var result = _unitOfWork
                .RoleRepository
                .Entities
                .Any(co => co.Name == name);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var result = PolicyTypes
                .Properties
                .Select(pt => new Common.DTO.Claim
                {
                    Type = SiteClaimTypes.Permission,
                    Value = pt
                })
                .ToList();

            return new ObjectResult(result);
        }

        [HttpPost("search")]
        public IActionResult GetRoles([FromBody] FilterCriteria criteria)
        {
            var query = _unitOfWork
                .RoleRepository
                .Entities;

            if(criteria != null && criteria.ItemsPerPage > 0)
            {
                if (criteria.Name != null)
                {
                    query = query.Where(pt => pt.Name.Contains(criteria.Name));
                }

                query = query
                    .Skip(criteria.Page * criteria.ItemsPerPage)
                    .Take(criteria.ItemsPerPage);
            }

            var result = query
                .Select(ro => new Common.DTO.Role
            {
                Id = ro.Id,
                Name = ro.Name
            });

            return new ObjectResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> AddRole([FromBody] AddRole model)
        {
            var role = new Common.DTO.Role()
            {
                Name = model.Name,
                Claims = model.Claims
            };

            await _unitOfWork.AddRole(role);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole([FromBody] EditRole model)
        {
            var role = new Common.DTO.Role()
            {
                Id = model.Id,
                Name = model.Name,
                Claims = model.Claims
            };

            await _unitOfWork.EditRole(role);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _unitOfWork.RoleRepository.Get(id);

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