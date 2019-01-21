using Common.IUnitOfWork;
using WebApiServer;

namespace Client.Services.Mock
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}