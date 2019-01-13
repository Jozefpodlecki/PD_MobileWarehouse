using System;
using System.Collections.Generic;
using Client.Models;
using Client.Providers.Interfaces;
using Common;
using WebApiServer.Models;

namespace Client.Providers.Mock
{
    public class PersistenceProvider : IPersistenceProvider
    {
        private string _language { get; set; }
        private string _token { get; set; }
        private Jwt _jwt { get; set; }
        private Login _login { get; set; }
        private IEnumerable<string> _claims { get; set; }

        public PersistenceProvider()
        {
            _language = "Polish";
            _token = null;
            _jwt = null;
            _login = null;
        }

        public void ClearCredentials()
        {
            _login = null;
        }

        public void ClearToken()
        {
            _token = null;
            _jwt = null;
        }

        public Login GetCredentials()
        {
            return _login;
        }

        public string GetEncryptedToken()
        {
            return _token;   
        }

        public string GetLanguage()
        {
            return _language;
        }

        public IEnumerable<string> GetPermissionClaims()
        {
            return _claims;
        }

        public Jwt GetToken()
        {
            return _jwt;
        }

        public void SaveToken(AppSettings appSettings, string encryptedToken)
        {
            _jwt = new Jwt
            {

            };

            _token = "";
        }

        public void SetCredentials(Login model)
        {
            _login = model;
        }

        public void SetLanguage(string language)
        {
            _language = language;
        }
    }
}