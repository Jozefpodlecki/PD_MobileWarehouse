using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
    public class LocationRepository : NameRepository<Location>, ILocationRepository
    {
        public LocationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Location> GetLocationsByProduct(string name)
        {
            return Entities
                .Where(lo => lo.ProductDetails
                    .Any(pd => pd.Product.Name
                        .Contains(name, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
