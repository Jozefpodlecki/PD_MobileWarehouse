using Client.Services.Interfaces;
using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ProductService : Service, IProductService
    {
        public ProductService(HttpClientManager httpClientManager, HttpHelper httpHelper, string postFix) : base(httpClientManager, httpHelper, postFix)
        {
        }

        public async Task<HttpResult<Models.Product>> GetProductByBarcode(string barcode, CancellationToken token = default(CancellationToken))
        {
            return await Post<Models.Product>(barcode, "/barcode", token);
        }

        public async Task<HttpResult<bool>> UpdateProduct(Models.Product entity, CancellationToken token = default(CancellationToken))
        {
            return await Post(entity, token);
        }

        public async Task<HttpResult<List<Models.Product>>> GetProducts(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.Product>(criteria, token);
        }
    }
}