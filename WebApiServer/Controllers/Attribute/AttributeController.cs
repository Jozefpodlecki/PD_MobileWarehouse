﻿using Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiServer.Controllers.Attribute.ViewModel;

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

        [HttpPut]
        public async Task<IActionResult> AddAttribute([FromBody] AddAttribute model)
        {
            await _unitOfWork.AddAttribute(model);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditAttribute([FromBody] EditAttribute model)
        {
            await _unitOfWork.EditAttribute(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            await _unitOfWork.DeleteAttribute(id);

            return Ok();
        }

        [HttpPost("search")]
        public async Task <IActionResult> GetAttributes([FromBody] FilterCriteria criteria)
        {
            return new ObjectResult(await _unitOfWork.GetAttributes(criteria));
        }
    }
}