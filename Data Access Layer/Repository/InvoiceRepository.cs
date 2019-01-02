using Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public class InvoiceRepository : Repository<Invoice>
    {
        public static List<KeyValue> PaymentMethods;
        public static List<KeyValue> InvoiceTypes;

        public InvoiceRepository(DbContext dbContext) : base(dbContext)
        {
        }

        static InvoiceRepository()
        {
            PaymentMethods = Common.Helpers.MakeKeyValuePairFromEnum<byte>(typeof(PaymentMethod));
            InvoiceTypes = Common.Helpers.MakeKeyValuePairFromEnum<byte>(typeof(InvoiceType));
        }
    }
}
