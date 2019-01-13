using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Client.SQLite;
using Common;
using Microsoft.EntityFrameworkCore;

namespace Client.Services.Mock
{
    public class NoteService : BaseSQLiteService<Models.GoodsReceivedNote>, INoteService
    {
        private readonly DbSet<Models.GoodsDispatchedNote> _dbGoodsDispatchedNoteSet;

        public NoteService(SQLiteDbContext sqliteDbContext) : base(sqliteDbContext)
        {
        }

        public Task<HttpResult<bool>> AddGoodsDispatchedNote(GoodsDispatchedNote note, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<bool>> AddGoodsReceivedNote(GoodsReceivedNote note, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<GoodsDispatchedNote>>> GetGoodsDispatchedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<List<GoodsReceivedNote>>> GetGoodsReceivedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}