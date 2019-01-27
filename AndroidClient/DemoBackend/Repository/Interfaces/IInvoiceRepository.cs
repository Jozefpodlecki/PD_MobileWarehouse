using Common;
using Data_Access_Layer;
using System.Collections.Generic;

namespace Common.Repository.Interfaces
{
    public interface IInvoiceRepository : INameRepository<Invoice>
    {
        List<KeyValue> GetPaymentMethods();
        List<KeyValue> GetInvoiceTypes();
        IEnumerable<Invoice> Get(InvoiceFilterCriteria criteria);
    }
}