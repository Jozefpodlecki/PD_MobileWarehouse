using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using WebApiServer.Controllers.Counterparty.ViewModel;

namespace Client.Services.Mock
{
    public class CounterpartyService : BaseService, ICounterpartyService
    {
        public CounterpartyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<bool>> AddCounterparty(Counterparty counterparty, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new AddCounterparty
            {
                Name = counterparty.Name,
                NIP = counterparty.NIP,
                City = new Common.DTO.City
                {
                    Id = counterparty.City.Id,
                    Name = counterparty.City.Name
                },
                PhoneNumber = counterparty.PhoneNumber,
                PostalCode = counterparty.PostalCode,
                Street = counterparty.Street
            };

            await _unitOfWork.AddCounterparty(model);

            return result;
        }

        public async Task<HttpResult<bool>> CounterpartyExists(string name, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            return result;
        }

        public async Task<HttpResult<bool>> DeleteCounterparty(int id, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            return result;
        }

        public async Task<HttpResult<List<Counterparty>>> GetCounterparties(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Counterparty>>();

            try
            {
                result.Data = _unitOfWork
                .GetCounterparties(criteria)
                .Select(co => new Counterparty
                {
                    Id = co.Id,
                    Name = co.Name,
                    PostalCode = co.PostalCode,
                    Street = co.Street,
                    NIP = co.NIP,
                    PhoneNumber = co.PhoneNumber,
                    CreatedAt = co.CreatedAt,
                    CreatedBy = co.CreatedBy == null ? null : new User
                    {
                        Id = co.CreatedBy.Id,
                        Username = co.CreatedBy.Username
                    },
                    LastModifiedBy = co.LastModifiedBy == null ? null : new User
                    {
                        Id = co.LastModifiedBy.Id,
                        Username = co.LastModifiedBy.Username
                    },
                    LastModifiedAt = co.LastModifiedAt
                })
                .ToList();
            }
            catch (Exception ex)
            {
                
            }
            

            return result;
        }

        public async Task<HttpResult<bool>> NIPExists(string nip, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            return result;
        }

        public async Task<HttpResult<bool>> UpdateCounterparty(Counterparty counterparty, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new EditCounterparty
            {
                Id = counterparty.Id,
                Name = counterparty.Name,
                NIP = counterparty.NIP,
                PhoneNumber = counterparty.PhoneNumber,
                PostalCode = counterparty.PostalCode,
                Street = counterparty.Street,
                City = new Common.DTO.City
                {
                    Id = counterparty.City.Id,
                    Name = counterparty.City.Name
                }
            };

            await _unitOfWork.UpdateCounterparty(model);

            return result;
        }
    }
}