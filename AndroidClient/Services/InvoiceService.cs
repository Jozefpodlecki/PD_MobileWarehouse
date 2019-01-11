using Client.Services.Interfaces;
using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class InvoiceService : Service, IInvoiceService
    {
        public InvoiceService() : base("/api/invoice")
        {
        }

        public async Task<HttpResult<List<Models.KeyValue>>> GetPaymentMethods(CancellationToken token = default(CancellationToken))
        {
            return await Get<Models.KeyValue>("/paymentMethods", token);
        }

        public async Task<HttpResult<List<Models.KeyValue>>> GetInvoiceTypes(CancellationToken token = default(CancellationToken))
        {
            return await Get<Models.KeyValue>("/invoiceTypes", token);
        }

        public async Task<HttpResult<List<Models.Invoice>>> GetInvoices(InvoiceFilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.Invoice>(criteria, token);
        }

        public async Task<HttpResult<bool>> AddInvoice(Models.Invoice invoice, CancellationToken token = default(CancellationToken))
        {
            return await Put(invoice, token);
        }

        public async Task<HttpResult<bool>> DeleteInvoice(int id, CancellationToken token = default(CancellationToken))
        {
            return await Delete(id, token);
        }
    }
}