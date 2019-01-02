using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class LocationRepository : NameRepository<Location>
    {
        public LocationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Location> GetLocationsByProduct(string name)
        {
            return Entities
                .Where(lo => lo.ProductDetails
                    .Any(pd => pd.Product.Name
                        .Contains(name, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
