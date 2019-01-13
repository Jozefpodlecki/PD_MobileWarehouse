using Client.Providers;

namespace Client.Services.Interfaces
{
    public interface IService
    {
        void SetAuthorizationHeader(PersistenceProvider persistenceProvider);
    }
}