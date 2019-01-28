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

            if(entity != null)
            {
                result.Data = new Product
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Avatar = entity.Image,
                    Barcode = entity.Barcode,
                    CreatedAt = entity.CreatedAt,
                    ProductAttributes = entity
                        .ProductAttributes
                        .Select(pa => new Models.ProductAttribute
                        {
                            Attribute = new Models.Attribute
                            {
                                Id = pa.Attribute.Id,
                                Name = pa.Attribute.Name
                            },
                            Value = pa.Value
                        })
                        .ToList(),
                    ProductDetails = entity
                        .ProductDetails
                        .Select(pd => new ProductDetail
                        {
                            Location = new Location
                            {
                                Id = pd.Location.Id,
                                Name = pd.Location.Name
                            },
                            Count = pd.Count
                        })
                        .ToList(),
                    CreatedBy = entity.CreatedBy == null ? null : new User
                    {
                        Id = entity.CreatedBy.Id,
                        Username = entity.CreatedBy.Username
                    },
                    LastModifiedBy = entity.LastModifiedBy == null ? null : new User
                    {
                        Id = entity.LastModifiedBy.Id,
                        Username = entity.LastModifiedBy.Username
                    },
                    LastModifiedAt = entity.LastModifiedAt
                };
            }

            return result;
        }

        public async Task<HttpResult<List<Product>>> GetProducts(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Product>>();

            result.Data = _unitOfWork
                .GetProducts(criteria)
                .Select(pr => new Product
                {
                    Id = pr.Id,
                    Avatar = pr.Image,
                    Barcode = pr.Barcode,
                    Name = pr.Name,
                    ProductAttributes = pr
                        .ProductAttributes
                        .Select(pa => new Models.ProductAttribute
                        {
                            Attribute = new Models.Attribute
                            {
                                Id = pa.Attribute.Id,
                                Name = pa.Attribute.Name
                            },
                            Value = pa.Value
                        })
                        .ToList(),
                    ProductDetails = pr
                        .ProductDetails
                        .Select(pd => new ProductDetail
                        {
                            Location = new Location
                            {
                                Id = pd.Location.Id,
                                Name = pd.Location.Name
                            },
                            Count = pd.Count
                        })
                        .ToList(),
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
                    LastModifiedAt = pr.LastModifiedAt
                })
                .ToList();

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