using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;

namespace Client.Services.Mock
{
    public class CityService : BaseService, ICityService
    {
        public CityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<List<City>>> GetCities(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<City>>();

            result.Data = _unitOfWork
                .GetCities(criteria)
                .Select(ci => new Models.City
                {
                    Id = ci.Id,
                    Name = ci.Name
                })
                .ToList();

            return result;
        }
    }
}