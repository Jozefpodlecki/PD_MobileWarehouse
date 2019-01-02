using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class EntryRepository : NameRepository<Entry>
    {
        public EntryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Entry>> GetForInvoice(int invoiceId)
        {
            return await Entities
                .Where(en => en.InvoiceId == invoiceId)
                .ToListAsync();
        }
    }
}
