using Common;
using Data_Access_Layer.Repository;
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
        public readonly INameRepository<Data_Access_Layer.Location> _locationRepository;

        public LocationController(
            INameRepository<Data_Access_Layer.Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpHead]
        public async Task<IActionResult> LocationExists([FromQuery] string name)
        {
            var result = await _locationRepository.Exists(name);

            if(result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult GetLocations()
        {
            var result = _locationRepository
                .Entities;

            return new ObjectResult(result);
        }

        [HttpPost("search")]
        public IActionResult GetLocations([FromBody] FilterCriteria criteria)
        {
            var result = _locationRepository
               .Entities;

            var entities = Helpers.Paging.GetPaged(result, criteria)
                .ToList();

            return new ObjectResult(entities);
        }

        [HttpPut]
        public async Task<IActionResult> AddLocation([FromBody] AddLocation model)
        {
            var location = new Data_Access_Layer.Location()
            {
                Name = model.Name
            };

            await _locationRepository.Add(location);
            await _locationRepository.Save();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditLocation([FromBody] EditLocation model)
        {
            var location = new Data_Access_Layer.Location()
            {
                Id = model.Id,
                Name = model.Name
            };

            _locationRepository.Update(location);
            await _locationRepository.Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            var location = await _locationRepository.Get(id);
            _locationRepository.Remove(location);

            await _locationRepository.Save();

            return Ok();
        }
    }
}