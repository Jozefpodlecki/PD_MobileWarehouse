using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Client.Providers.Interfaces;
using Client.Services.Interfaces;
using WebApiServer.Models;

namespace Client.Services
{
    public class HttpClientManager : IHttpClientManager
    {
        public readonly HttpClient HttpClient;
        public string BaseUrl { get; set; }
        protected string Token;
        protected Jwt Jwt;

        public HttpClientManager()
        {
            HttpClient = new HttpClient();
        }

        public void ClearAuthorizationHeader()
        {
            HttpClient.DefaultRequestHeaders.Authorization = null;
        }

        public void SetAuthorizationHeader(IPersistenceProvider persistenceProvider)
        {
            Token = persistenceProvider.GetEncryptedToken();
            Jwt = persistenceProvider.GetToken();

            var authHeader = new AuthenticationHeaderValue("Bearer", Token);
            HttpClient.DefaultRequestHeaders.Authorization = authHeader;
        }

        public bool CheckJwt()
        {
            if(Jwt == null)
            {
                return false;
            }

            var expirationTime = DateTimeOffset
                .FromUnixTimeSeconds(int.Parse(Jwt.ExpirationTime))
                .UtcDateTime;

            var currentUtcDate = DateTime.UtcNow;

            if (currentUtcDate > expirationTime)
            {
                ClearAuthorizationHeader();

                return false;
            }

            return true;
        }
    }
}