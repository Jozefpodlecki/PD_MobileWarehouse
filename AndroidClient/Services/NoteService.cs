
using Android.App;
using Common;
using Common.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class NoteService : Service
    {
        public NoteService(
            Activity activity
            ) : base(activity, "api/note")
        {
        }

        public async Task<HttpResult<List<Models.City>>> GetCities(string name,CancellationToken token = default(CancellationToken))
        {
            return await Get<Models.City>("/cities","name",name, token);
        }

        public async Task<HttpResult<List<Models.City>>> GetCities(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.City>(criteria, "/cities", token);
        }

        public async Task<HttpResult<List<Models.KeyValue>>> GetPaymentMethods(CancellationToken token = default(CancellationToken))
        {
            return await Get<Models.KeyValue>("/invoice/paymentMethods", token);
        }

        public async Task<HttpResult<List<Models.KeyValue>>> GetInvoiceTypes(CancellationToken token = default(CancellationToken))
        {
            return await Get<Models.KeyValue>("/invoice/invoiceTypes", token);
        }

        public async Task<HttpResult<bool>> CounterpartyExists(string name, CancellationToken token = default(CancellationToken))
        {
            return await Exists("name",name,"/counterparty");
        }

        public async Task<HttpResult<bool>> NIPExists(string nip, CancellationToken token = default(CancellationToken))
        {
            return await Exists("nip", nip, "/counterparty");
        }

        public async Task<HttpResult<bool>> DeleteCounterparty(int id, CancellationToken token = default(CancellationToken))
        {
            return await Delete(id, "/counterparty");
        }

        public async Task<HttpResult<bool>> AddCounterparty(Counterparty model, CancellationToken token = default(CancellationToken))
        {
            return await Put(model, "/counterparty");
        }

        public async Task<HttpResult<List<Models.Counterparty>>> GetCounterparties(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.Counterparty>(criteria, "/counterparties", token);
        }

        public async Task<HttpResult<List<GoodsReceivedNote>>> GetGoodsReceivedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<GoodsReceivedNote>(criteria, "/goodsReceivedNotes", token);        
        }

        public async Task<HttpResult<List<GoodsDispatchedNote>>> GetGoodsDispatchedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<GoodsDispatchedNote>(criteria, "/goodsDispatchedNotes", token);       
        }

        public async Task<HttpResult<List<Invoice>>> GetInvoices(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Invoice>(criteria, "/invoices", token);
        }

        public async Task<HttpResult<bool>> AddInvoice(Invoice invoice, CancellationToken token = default(CancellationToken))
        {
            return await Put(invoice, "/invoice", token);
        }
    }
}