using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data_Access_Layer.Repository
{
    public class AttributeRepository : NameRepository<Attribute>
    {
        public AttributeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
