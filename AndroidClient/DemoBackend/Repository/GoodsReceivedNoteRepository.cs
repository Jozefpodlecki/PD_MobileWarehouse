using Data_Access_Layer;
using Common.Repository.Interfaces;

namespace Client.Repository
{
    public class GoodsReceivedNoteRepository : NameRepository<GoodsReceivedNote>, IGoodsReceivedNoteRepository
    {
        public GoodsReceivedNoteRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public override GoodsReceivedNote Get(int id)
        {
            return _sqliteConnection
                .Table<GoodsReceivedNote>()
                .FirstOrDefault(grn => grn.InvoiceId == id);
        }
    }
}