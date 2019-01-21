using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Client.Providers.Interfaces;
using Client.Services.Interfaces;
using WebApiServer.Models;

namespace Client.Services
{
    public class HttpClientAuthorizationManager : IAuthorizationManager
    {
        public readonly HttpClient HttpClient;
        public string BaseUrl { get; set; }
        private string _token;
        private Jwt _jwt;

        public HttpClientAuthorizationManager()
        {
            HttpClient = new HttpClient();
        }

        public void ClearAuthorization()
        {
            HttpClient.DefaultRequestHeaders.Authorization = null;
            _token = null;
            _jwt = null;
        }

        public void SetAuthorization(object data)
        {
            var arr = (object[])data;
            
            _token = (string)arr[0];
            _jwt = (Jwt)arr[1];
            BaseUrl = (string)arr[2];

            var authHeader = new AuthenticationHeaderValue("Bearer", _token);
            HttpClient.DefaultRequestHeaders.Authorization = authHeader;
        }

        public bool CheckAuthorization()
        {
            if(_jwt == null)
            {
                return false;
            }

            var expirationTime = DateTimeOffset
                .FromUnixTimeSeconds(int.Parse(_jwt.ExpirationTime))
                .UtcDateTime;

            var currentUtcDate = DateTime.UtcNow;

            if (currentUtcDate > expirationTime)
            {
                ClearAuthorization();

                return false;
            }

            return true;
        }
    }
}