using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using WebApiServer.Controllers.Location.ViewModel;

namespace Client.Services.Mock
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<bool>> AddLocation(Location location, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new AddLocation
            {
                Name = location.Name
            };

            await _unitOfWork.AddLocation(model);

            return result;
        }

        public Task<HttpResult<bool>> DeleteLocation(int id, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();
            result.Data = true;

            return Task.FromResult(result);
        }

        public async Task<HttpResult<List<Location>>> GetLocations(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Location>>();

            result.Data = _unitOfWork
                .GetLocations(criteria)
                .Select(lo => new Location
                {
                    Id = lo.Id,
                    Name = lo.Name,
                    CreatedAt = lo.CreatedAt,
                    CreatedBy = lo.CreatedBy == null ? null : new User
                    {
                        Id = lo.CreatedBy.Id,
                        Username = lo.CreatedBy.Username
                    },
                    LastModifiedBy = lo.LastModifiedBy == null ? null : new User
                    {
                        Id = lo.LastModifiedBy.Id,
                        Username = lo.LastModifiedBy.Username
                    },
                    LastModifiedAt = lo.LastModifiedAt
                })
                .ToList();

            return result;
        }

        public Task<HttpResult<List<Location>>> GetLocationsByProduct(string name, string value, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Location>>();

            result.Data = _unitOfWork
                .GetLocationsByProduct(name)
                .Select(lo => new Location
                {
                    Id = lo.Id,
                    Name = lo.Name,
                    CreatedAt = lo.CreatedAt,
                    CreatedBy = lo.CreatedBy == null ? null : new User
                    {
                        Id = lo.CreatedBy.Id,
                        Username = lo.CreatedBy.Username
                    },
                    LastModifiedBy = lo.LastModifiedBy == null ? null : new User
                    {
                        Id = lo.LastModifiedBy.Id,
                        Username = lo.LastModifiedBy.Username
                    },
                    LastModifiedAt = lo.LastModifiedAt
                })
                .ToList();

            return Task.FromResult(result);
        }

        public async Task<HttpResult<bool>> LocationExists(string name, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            result.Data = await _unitOfWork.LocationExists(name);

            return result;
        }

        public async Task<HttpResult<bool>> UpdateLocation(Location location, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new EditLocation
            {
                Id = location.Id,
                Name = location.Name
            };

            await _unitOfWork.EditLocation(model);

            return result;
        }
    }
}