using Common;
using Common.IUnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiServer.Controllers.Attribute.ViewModel;

namespace WebApiServer.Controllers.Attribute
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;

        public AttributeController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = SiteClaimValues.Attributes.Add)]
        [HttpPut]
        public async Task<IActionResult> AddAttribute([FromBody] AddAttribute model)
        {
            await _unitOfWork.AddAttribute(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Attributes.Update)]
        [HttpPost]
        public async Task<IActionResult> EditAttribute([FromBody] EditAttribute model)
        {
            await _unitOfWork.EditAttribute(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Attributes.Remove)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            await _unitOfWork.DeleteAttribute(id);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Attributes.Read)]
        [HttpPost("search")]
        public async Task <IActionResult> GetAttributes([FromBody] FilterCriteria criteria)
        {
            return new ObjectResult(await _unitOfWork.GetAttributes(criteria));
        }
    }
}