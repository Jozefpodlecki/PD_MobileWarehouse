using System.Collections.Generic;
using System.Threading.Tasks;
using Data_Access_Layer;

namespace Common.Repository.Interfaces
{
    public interface IEntryRepository : IRepository<Entry>
    {
        Task<List<Entry>> GetForInvoice(int invoiceId);
    }
}