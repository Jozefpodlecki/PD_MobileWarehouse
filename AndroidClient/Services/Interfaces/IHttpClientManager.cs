using Client.Providers;
using Client.Providers.Interfaces;

namespace Client.Services.Interfaces
{
    public interface IHttpClientManager
    {
        string BaseUrl { get; set; }

        void ClearAuthorizationHeader();

        void SetAuthorizationHeader(IPersistenceProvider persistenceProvider);

        bool CheckJwt();
    }
}