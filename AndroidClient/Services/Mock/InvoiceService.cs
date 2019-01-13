using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Client.SQLite;
using Common;

namespace Client.Services.Mock
{
    public class InvoiceService : BaseSQLiteService<Models.Invoice>, IInvoiceService
    {
        public InvoiceService(SQLiteDbContext sqliteDbContext) : base(sqliteDbContext)
        {
        }

        public Task<HttpResult<bool>> AddInvoice(Invoice invoice, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> DeleteInvoice(int id, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<Invoice>>> GetInvoices(InvoiceFilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<Models.KeyValue>>> GetInvoiceTypes(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<Models.KeyValue>>> GetPaymentMethods(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}