using Client.Providers;
using Client.Providers.Interfaces;
using Client.Services.Interfaces;

namespace Client.Services.Mock
{
    public class HttpClientManager : IHttpClientManager
    {
        public string BaseUrl { get; set; }

        public bool CheckJwt()
        {
            return true;
        }

        public void ClearAuthorizationHeader()
        {
            
        }

        public void SetAuthorizationHeader(IPersistenceProvider persistenceProvider)
        {
            
        }
    }
}