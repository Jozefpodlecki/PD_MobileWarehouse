using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data_Access_Layer.Repository
{
    public class ProductDetailsRepository : Repository<ProductDetail>
    {
        public ProductDetailsRepository(DbContext dbContext) : base(dbContext)
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
