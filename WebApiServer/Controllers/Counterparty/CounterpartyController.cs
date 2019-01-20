using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiServer.Controllers.Counterparty.ViewModel;

namespace WebApiServer.Controllers.Counterparty
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterpartyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CounterpartyController(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = SiteClaimValues.Counterparties.Read)]
        [HttpPost("search")]
        public IActionResult GetCounterparties([FromBody] FilterCriteria criteria)
        {
            var result = _unitOfWork.GetCounterparties(criteria);
            return new ObjectResult(result);
        }

        [Authorize(Policy = SiteClaimValues.Counterparties.Update)]
        [HttpPost]
        public async Task<IActionResult> UpdateCounterparty([FromBody] EditCounterparty model)
        {
            await _unitOfWork.UpdateCounterparty(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Counterparties.Add)]
        [HttpPut]
        public async Task<IActionResult> AddCounterparty([FromBody] AddCounterparty model)
        {
            await _unitOfWork.AddCounterparty(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Counterparties.Read)]
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