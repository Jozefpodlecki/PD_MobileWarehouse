using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class EntryRepository : NameRepository<Entry>, IEntryRepository
    {
        public EntryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Entry>> GetForInvoice(int invoiceId)
        {
            return Entities
                .Where(en => en.InvoiceId == invoiceId)
                .ToList();
        }
    }
}
