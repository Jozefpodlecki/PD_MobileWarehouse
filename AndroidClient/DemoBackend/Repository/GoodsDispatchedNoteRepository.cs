using Data_Access_Layer;
using Common.Repository.Interfaces;

namespace Client.Repository
{
    public class GoodsDispatchedNoteRepository : NameRepository<GoodsDispatchedNote>, IGoodsDispatchedNoteRepository
    {
        public GoodsDispatchedNoteRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public override GoodsDispatchedNote Get(int id)
        {
            return _sqliteConnection
                .Table<GoodsDispatchedNote>()
                .FirstOrDefault(grn => grn.InvoiceId == id);
        }

    }
}