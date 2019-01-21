using Common;
using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private static List<KeyValue> PaymentMethods;
        private static List<KeyValue> InvoiceTypes;

        public InvoiceRepository(DbContext dbContext) : base(dbContext)
        {
        }

        static InvoiceRepository()
        {
            PaymentMethods = Common.Helpers.MakeKeyValuePairFromEnum<byte>(typeof(PaymentMethod));
            InvoiceTypes = Common.Helpers.MakeKeyValuePairFromEnum<byte>(typeof(InvoiceType));
        }

        public List<KeyValue> GetPaymentMethods()
        {
            return PaymentMethods;
        }

        public List<KeyValue> GetInvoiceTypes()
        {
            return InvoiceTypes;
        }
    }
}
