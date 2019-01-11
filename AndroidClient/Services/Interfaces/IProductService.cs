using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Client.Services.Interfaces
{
    public interface IProductService
    {
        Task<HttpResult<Models.Product>> GetProductByBarcode(string barcode, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> UpdateProduct(Models.Product entity, CancellationToken token = default(CancellationToken));

        Task<HttpResult<List<Models.Product>>> GetProducts(FilterCriteria criteria, CancellationToken token = default(CancellationToken));
    }
}