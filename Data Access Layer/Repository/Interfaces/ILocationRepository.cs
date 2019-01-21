using Data_Access_Layer;
using System.Collections.Generic;
using System.Linq;

namespace Common.Repository.Interfaces
{
    public interface ILocationRepository : INameRepository<Location>
    {
        IEnumerable<Location> GetLocationsByProduct(string name);
    }
}