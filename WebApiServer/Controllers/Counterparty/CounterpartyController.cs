using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using WebApiServer.Controllers.Counterparty.ViewModel;

namespace WebApiServer.Controllers.Counterparty
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterpartyController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CounterpartyController(
            UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("search")]
        public IActionResult GetCounterparties([FromBody] FilterCriteria criteria)
        {
            var result = _unitOfWork.GetCounterparties(criteria);
            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCounterparty([FromBody] EditCounterparty model)
        {
            await _unitOfWork.UpdateCounterparty(model);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddCounterparty([FromBody] AddCounterparty model)
        {
            await _unitOfWork.AddCounterparty(model);

            return Ok();
        }

        [HttpHead]
        public IActionResult Exists([FromQuery] ExistsCounterparty model)
        {
            var result = _unitOfWork.ExistsCounterparty(model);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}