using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.IUnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiServer.Controllers.Product.ViewModel;

namespace WebApiServer.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;

        public ProductController(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = SiteClaimValues.Products.Read)]
        [HttpHead]
        public IActionResult ProductExists([FromQuery]string name)
        {
            var exists = _unitOfWork.ProductExists(name);

            if (exists)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Policy = SiteClaimValues.Products.Read)]
        [HttpPost("barcode")]
        public async Task<IActionResult> GetProductByBarcode([FromBody]string barcode)
        {
            var item = _unitOfWork.GetProductByBarcode(barcode);

            if (item == null)
            {
                return NotFound();
            }

            return new OkObjectResult(item);
        }

        [Authorize(Policy = SiteClaimValues.Products.Update)]
        [HttpPost]
        public async Task<IActionResult> EditProduct([FromBody] EditProduct model)
        {
            await _unitOfWork.EditProduct(model);

            return Ok();
        }

        [Authorize(Policy = SiteClaimValues.Products.Read)]
        [HttpPost("search")]
        public IActionResult GetProducts([FromBody] FilterCriteria criteria)
        {
            return new ObjectResult(_unitOfWork
                .GetProducts(criteria));
        }

    }
}
