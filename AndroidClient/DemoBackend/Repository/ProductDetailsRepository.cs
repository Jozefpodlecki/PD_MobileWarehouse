using Common.Repository;
using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Client.Repository
{
    public class ProductDetailsRepository : Repository<ProductDetail>, IProductDetailsRepository
    {
        public ProductDetailsRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public List<ProductDetail> GetForProduct(int productId)
        {
            return Entities
                .Where(en => en.ProductId == productId)
                .ToList();
        }
    }
}
