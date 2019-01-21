using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class ProductDetailsRepository : Repository<ProductDetail>, IProductDetailsRepository
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

        Task IRepository<ProductDetail>.Add(ProductDetail entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
