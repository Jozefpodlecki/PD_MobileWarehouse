using Common;
using Common.Repository;
using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Client.Repository
{
    public class InvoiceRepository : NameRepository<Invoice>, IInvoiceRepository
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

        public IEnumerable<Invoice> Get(InvoiceFilterCriteria criteria)
        {
            var query = _sqliteConnection
                .GetAllWithChildren<Invoice>()
                .Skip(criteria.ItemsPerPage * criteria.Page)
                .Take(criteria.ItemsPerPage);

            if (criteria.InvoiceType.HasValue)
            {
                query.Where(inv => inv.InvoiceType == criteria.InvoiceType.Value);
            }

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query.Where(inv => inv.DocumentId == criteria.Name);
            }

            return query;
        }
    }
}
