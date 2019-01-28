using Common.Repository.Interfaces;
using Data_Access_Layer;
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

        public bool IsEmptyLocation(int locationId)
        {
            return Entities
                .Any(en => en.LocationId == locationId);
        }
    }
}
