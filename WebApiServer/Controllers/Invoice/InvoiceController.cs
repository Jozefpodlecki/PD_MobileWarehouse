using Common;
using Common.IUnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiServer.Controllers.Invoice
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceController(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = SiteClaimValues.Invoices.Read)]
        [HttpGet("paymentMethods")]
        public IActionResult GetPaymentMethods()
        {
            return new ObjectResult(_unitOfWork.GetPaymentMethods());
        }

        [Authorize(Policy = SiteClaimValues.Invoices.Read)]
        [HttpGet("invoiceTypes")]
        public IActionResult GetInvoiceTypes()
        {
            return new ObjectResult(_unitOfWork.GetInvoiceTypes());
        }

        [Authorize(Policy = SiteClaimValues.Invoices.Add)]
        [HttpPut]
        public async Task<IActionResult> AddInvoice([FromBody] ViewModel.AddInvoice model)
        {
            await _unitOfWork.CreateInvoice(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Invoices.Add)]
        [HttpPut("bulk")]
        public async Task<IActionResult> AddInvoice([FromBody] IEnumerable<ViewModel.AddInvoice> model)
        {
            await _unitOfWork.CreateInvoices(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Invoices.Read)]
        [HttpPost("search")]
        public async Task<IActionResult> GetInvoices([FromBody] InvoiceFilterCriteria criteria)
        {
            var entities = _unitOfWork.GetInvoices(criteria);

            return new ObjectResult(entities);
        }
    }
}