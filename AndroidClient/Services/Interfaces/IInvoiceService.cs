using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Client.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<HttpResult<List<Models.KeyValue>>> GetPaymentMethods(CancellationToken token = default(CancellationToken));

        Task<HttpResult<List<Models.KeyValue>>> GetInvoiceTypes(CancellationToken token = default(CancellationToken));

        Task<HttpResult<List<Models.Invoice>>> GetInvoices(InvoiceFilterCriteria criteria, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> AddInvoice(Models.Invoice invoice, CancellationToken token = default(CancellationToken));

        Task<HttpResult<bool>> DeleteInvoice(int id, CancellationToken token = default(CancellationToken));
     
    }
}