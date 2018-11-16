using Common;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [HttpGet]
        public IActionResult GetLocations()
        {
            var result = _locationRepository
                .Entities;

            return new ObjectResult(result);
        }

        [HttpPost("search")]
        public IActionResult GetLocations(FilterCriteria criteria)
        {
            var result = _locationRepository
               .Entities;

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                result = result
                    .Where(cr => cr.Name.Contains(criteria.Name));
            }

            var entities = result
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage)
                .ToList();

            return new ObjectResult(entities);
        }

        [HttpPut]
        public IActionResult AddLocation([FromBody] AddLocation model)
        {
            var result = _locationRepository
                .Entities;

            return new ObjectResult(result);
        }

        [HttpPost]
        public IActionResult EditLocation([FromBody] EditLocation model)
        {
            var result = _locationRepository
                .Entities;

            return new ObjectResult(result);
        }
    }
}