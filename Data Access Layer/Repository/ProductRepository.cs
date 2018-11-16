using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Product Get(string name)
        {
            return _dbset.FirstOrDefault(pr => pr.Name == name);
        }

        public new void Remove(Product entity)
        {
            entity.IsDeleted = true;

            Update(entity);
        }
    }
}
