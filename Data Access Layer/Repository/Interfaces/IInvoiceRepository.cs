using Common;
using Data_Access_Layer;
using System.Collections.Generic;

namespace Common.Repository.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        List<KeyValue> GetPaymentMethods();
        List<KeyValue> GetInvoiceTypes();
    }
}