using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiServer.Controllers.City
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CityController(
            UnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = PolicyTypes.Cities.Read)]
        [HttpPost("search")]
        public IActionResult GetCities([FromBody] FilterCriteria criteria)
        {
            return new ObjectResult(_unitOfWork.GetCities(criteria));
        }
    }
}