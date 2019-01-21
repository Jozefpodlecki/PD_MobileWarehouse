using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services.Mock
{
    public class AttributeService : BaseService, IAttributeService
    {
        public AttributeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<bool>> AddAttribute(Attribute attribute, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new WebApiServer.Controllers.Attribute.ViewModel.AddAttribute
            {
                Name = attribute.Name
            };

            await _unitOfWork.AddAttribute(model);
            return result;
        }

        public Task<HttpResult<bool>> DeleteAttribute(int id, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();
            _unitOfWork.DeleteAttribute(id);
            return Task.FromResult(result);
        }

        public Task<HttpResult<bool>> EditAttribute(Attribute attribute, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();
            //_unitOfWork.UpdateAttribute();
            return Task.FromResult(result);
        }

        public async Task<HttpResult<List<Attribute>>> GetAttributes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Attribute>>();

            var entites = await _unitOfWork.GetAttributes(criteria);
            result.Data = entites
                .Select(at => new Attribute
                {
                    Id = at.Id,
                    Name = at.Name,

                }).ToList();

            return result;
        }
    }
}