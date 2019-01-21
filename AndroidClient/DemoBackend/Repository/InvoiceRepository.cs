using Common;
using Common.Repository;
using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using System.Collections.Generic;

namespace Client.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private static List<KeyValue> PaymentMethods;
        private static List<KeyValue> InvoiceTypes;

        static InvoiceRepository()
        {
            PaymentMethods = Common.Helpers.MakeKeyValuePairFromEnum<byte>(typeof(PaymentMethod));
            InvoiceTypes = Common.Helpers.MakeKeyValuePairFromEnum<byte>(typeof(InvoiceType));
        }

        public InvoiceRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public List<KeyValue> GetInvoiceTypes()
        {
            return InvoiceTypes;
        }

        public List<KeyValue> GetPaymentMethods()
        {
            return PaymentMethods;
        }
    }
}
