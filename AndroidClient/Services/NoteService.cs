using Client.Services.Interfaces;
using Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class NoteService : Service, INoteService
    {
        public NoteService(HttpClientAuthorizationManager httpClientManager, HttpHelper httpHelper, string postFix) : base(httpClientManager, httpHelper, postFix)
        {
        }

        public async Task<HttpResult<List<Models.GoodsReceivedNote>>> GetGoodsReceivedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.GoodsReceivedNote>(criteria, "/goodsReceivedNotes", token);        
        }

        public async Task<HttpResult<List<Models.GoodsDispatchedNote>>> GetGoodsDispatchedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.GoodsDispatchedNote>(criteria, "/goodsDispatchedNotes", token);       
        }

        public async Task<HttpResult<bool>> AddGoodsReceivedNote(Models.GoodsReceivedNote note, CancellationToken token = default(CancellationToken))
        {
            return await Put(note, "/goodsReceivedNote", token);
        }

        public async Task<HttpResult<bool>> AddGoodsDispatchedNote(Models.GoodsDispatchedNote note, CancellationToken token = default(CancellationToken))
        {
            return await Put(note, "/goodsDispatchedNote", token);
        }
    }
}