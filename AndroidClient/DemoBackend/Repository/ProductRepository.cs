using Common.Repository.Interfaces;
using Data_Access_Layer;

namespace Client.Repository
{
    public class ProductRepository : NameRepository<Product>, IProductRepository
    {
        public ProductRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }
    }
}