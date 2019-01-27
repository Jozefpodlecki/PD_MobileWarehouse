using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository.Interfaces;
using SQLite;
using Data_Access_Layer;

namespace Client.Repository
{
    public class EntryRepository : NameRepository<Entry>, IEntryRepository
    {
        public EntryRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public List<Entry> GetForInvoice(int invoiceId)
        {
            return Entities
                .Where(en => en.InvoiceId == invoiceId)
                .ToList();
        }
    }
}
