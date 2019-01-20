using Common;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApiServer.Controllers.Location.ViewModel;

namespace WebApiServer.Controllers.Location
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;

        public LocationController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = SiteClaimValues.Locations.Read)]
        [HttpHead]
        public async Task<IActionResult> LocationExists([FromQuery] string name)
        {
            var result = await _unitOfWork.LocationExists(name);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Policy = SiteClaimValues.Locations.Read)]
        [HttpGet("product")]
        public IActionResult GetLocationsByProduct([FromQuery]string name)
        {
            var result = _unitOfWork.GetLocationsByProduct(name);

            return new ObjectResult(result);
        }

        [Authorize(Policy = SiteClaimValues.Locations.Read)]
        [HttpPost("search")]
        public IActionResult GetLocations([FromBody] FilterCriteria criteria)
        {
            var result = _unitOfWork.GetLocations(criteria);

            return new ObjectResult(result);
        }

        [Authorize(Policy = SiteClaimValues.Locations.Add)]
        [HttpPut]
        public async Task<IActionResult> AddLocation([FromBody] AddLocation model)
        {
            await _unitOfWork.AddLocation(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Locations.Update)]
        [HttpPost]
        public async Task<IActionResult> EditLocation([FromBody] EditLocation model)
        {
            await _unitOfWork.EditLocation(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Locations.Remove)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            await _unitOfWork.DeleteLocation(id);

            return Ok();
        }
    }
}