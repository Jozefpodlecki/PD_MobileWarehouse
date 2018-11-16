using Microsoft.AspNetCore.Mvc;

namespace WebApiServer.Controllers.Attribute
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        public readonly UnitOfWork _unitOfWork;

        public AttributeController(
            UnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAttribute([FromQuery] string name)
        {
            return new ObjectResult(_unitOfWork.AttributeRepository.Get(name));
        }
    }
}