using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class ProductRepository : NameRepository<Product>
    {
        public ProductRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
