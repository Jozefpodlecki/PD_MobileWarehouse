using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.DTO;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiServer.Controllers.Product.ViewModel;

namespace WebApiServer.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly UnitOfWork _unitOfWork;

        public ProductController(
            UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpHead]
        public IActionResult ProductExists([FromQuery]string name)
        {
            var exists = _unitOfWork
                .ProductRepository
                .Entities
                .Any(pr => pr.Name == name);

            if (exists)
            {
                return Ok();
            }

            return NotFound();
        }



        [HttpPut]
        public async Task<IActionResult> AddProduct([FromBody] AddProduct model)
        {

            await _unitOfWork.AddProduct(model);

            _unitOfWork.Save();

            return new ObjectResult(null);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.Get(id);

            _unitOfWork.ProductRepository.Remove(product);

            _unitOfWork.Save();

            return new ObjectResult(null);
        }

        [HttpGet]
        public IActionResult GetProductsByName([FromQuery]string name)
        {
            var result = _unitOfWork
                .ProductRepository
                .Entities
                .Where(pr => pr.Name.Contains(name))
                .Select(pr => pr.Name)
                .ToList();

            return new ObjectResult(result);
        }

        [HttpPost("search")]
        public IActionResult GetProductsByCriteria([FromBody] FilterCriteria criteria)
        {
            return new ObjectResult(_unitOfWork
                .GetProducts(criteria));
        }
        /*
                [HttpPost("search")]
                public IActionResult GetProducts([FromBody]IEnumerable<string> names)
                {
                    var entity = _unitOfWork
                        .ProductRepository
                        .Entities
                        .Where(pr => names.Any(na => na == pr.Name))
                        .Select(pr => new BasicProduct
                         {
                             Id = pr.Id,
                             Name = pr.Name,
                             Image = pr.Image,
                             LastModification = pr.LastModification
                         })
                         .ToList();

                    return new ObjectResult(entity);
                }
        */

    }
}
