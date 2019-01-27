using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface INoteService
    {
        Task<HttpResult<List<Models.GoodsReceivedNote>>> GetGoodsReceivedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken));
        Task<HttpResult<List<Models.GoodsDispatchedNote>>> GetGoodsDispatchedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken));
        Task<HttpResult<bool>> AddGoodsReceivedNote(Models.GoodsReceivedNote note, CancellationToken token = default(CancellationToken));
        Task<HttpResult<bool>> AddGoodsDispatchedNote(Models.GoodsDispatchedNote note, CancellationToken token = default(CancellationToken));
        Task<HttpResult<bool>> DeleteGoodsReceivedNote(int invoiceId, CancellationToken token = default(CancellationToken));
        Task<HttpResult<bool>> DeleteGoodsDispatchedNote(int invoiceId, CancellationToken token = default(CancellationToken));
    }
}