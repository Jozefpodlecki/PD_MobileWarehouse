using Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiServer.Controllers.Invoice
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public InvoiceController(
            UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("paymentMethods")]
        public IActionResult GetPaymentMethods()
        {
            return new ObjectResult(_unitOfWork.GetPaymentMethods());
        }

        [HttpGet("invoiceTypes")]
        public IActionResult GetInvoiceTypes()
        {
            return new ObjectResult(_unitOfWork.GetInvoiceTypes());
        }

        [HttpPut]
        public async Task<IActionResult> AddInvoice([FromBody] ViewModel.AddInvoice model)
        {
            await _unitOfWork.CreateInvoice(model);

            return Ok();
        }

        [HttpPut("bulk")]
        public async Task<IActionResult> AddInvoice([FromBody] IEnumerable<ViewModel.AddInvoice> model)
        {
            await _unitOfWork.CreateInvoices(model);

            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetInvoices([FromBody] InvoiceFilterCriteria criteria)
        {
            var entities = await _unitOfWork.GetInvoices(criteria);

            return new ObjectResult(entities);
        }
    }
}