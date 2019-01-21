using Common.Repository;
using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Repository
{
    public class LocationRepository : NameRepository<Location>, ILocationRepository
    {
        public LocationRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public IEnumerable<Location> GetLocationsByProduct(string name)
        {
            return _sqliteConnection
                .Table<Location>()
                .Where(lo => lo.ProductDetails
                    .Any(pd => pd.Product.Name
                        .Contains(name, StringComparison.InvariantCultureIgnoreCase)))
                        .ToList();
        }
    }
}
