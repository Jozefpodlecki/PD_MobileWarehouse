using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data_Access_Layer.Repository
{
    public class AttributeRepository : Repository<Attribute>
    {
        public AttributeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Attribute Get(string name)
        {
            return _dbset.FirstOrDefault(pr => pr.Name == name);
        }
    }
}
