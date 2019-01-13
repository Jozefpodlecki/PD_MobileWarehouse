using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiServer.Controllers.Note.ViewModel;

namespace WebApiServer.Controllers.Note
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public NoteController(
            UnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = SiteClaimValues.Notes.Add)]
        [HttpPut("goodsReceivedNote")]
        public async Task<IActionResult> AddGoodsReceivedNote([FromBody] AddGoodsReceivedNote model)
        {
            await _unitOfWork.AddGoodsReceivedNote(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Notes.Add)]
        [HttpPut("goodsDispatchedNote")]
        public async Task<IActionResult> AddGoodsDispatchedNote([FromBody] AddGoodsDispatchedNote model)
        {
            await _unitOfWork.AddGoodsDispatchedNote(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Notes.Read)]
        [HttpPost("goodsReceivedNotes/search")]
        public async Task<IActionResult> GetGoodsReceivedNotes([FromBody] FilterCriteria criteria)
        {
            var result = _unitOfWork.GetGoodsReceivedNotes(criteria);

            return new ObjectResult(result);
        }

        [Authorize(Policy = SiteClaimValues.Notes.Read)]
        [HttpPost("goodsDispatchedNotes/search")]
        public async Task<IActionResult> GetGoodsDispatchedNotes([FromBody] FilterCriteria criteria)
        {
            var result = _unitOfWork.GetGoodsDispatchedNotes(criteria);

            return new ObjectResult(result);
        }
    }
}
