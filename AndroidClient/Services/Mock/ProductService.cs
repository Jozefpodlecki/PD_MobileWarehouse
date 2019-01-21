using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using WebApiServer.Controllers.Product.ViewModel;

namespace Client.Services.Mock
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<Product>> GetProductByBarcode(string barcode, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<Product>();

            var entity = _unitOfWork.GetProductByBarcode(barcode);

            result.Data = new Product
            {
                Id = entity.Id,
                Name = entity.Name,
                Avatar = entity.Image,
                Barcode = entity.Barcode,

            };

            return result;
        }

        public async Task<HttpResult<List<Product>>> GetProducts(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Product>>();
            try
            {
                result.Data = _unitOfWork
                .GetProducts(criteria)
                .Select(pr => new Product
                {
                    Id = pr.Id,
                    Avatar = pr.Image,
                    Barcode = pr.Barcode,
                    Name = pr.Name,
                    CreatedAt = pr.CreatedAt,
                    CreatedBy = pr.CreatedBy == null ? null : new User
                    {
                        Id = pr.CreatedBy.Id,
                        Username = pr.CreatedBy.Username
                    },
                    LastModifiedBy = pr.LastModifiedBy == null ? null : new User
                    {
                        Id = pr.LastModifiedBy.Id,
                        Username = pr.LastModifiedBy.Username
                    },
                    LastModifiedAt = pr.LastModifiedAt,

                })
                .ToList();
            }
            catch (System.Exception ex)
            {

                
            }

            

            return result;
        }

        public async Task<HttpResult<bool>> UpdateProduct(Product entity, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new EditProduct
            {
                Id = entity.Id,
                Image = entity.Avatar,
                ProductAttributes = entity
                    .ProductAttributes
                    .Select(pa => new WebApiServer.Controllers.Product.ViewModel.ProductAttribute
                    {
                        Attribute = new WebApiServer.Controllers.Product.ViewModel.Attribute
                        {
                            Id = pa.Attribute.Id,
                            Name = pa.Attribute.Name
                        },
                        Value = pa.Value
                    })
                    .ToList()
            };

            await _unitOfWork.EditProduct(model);

            return result;
        }
    }
}