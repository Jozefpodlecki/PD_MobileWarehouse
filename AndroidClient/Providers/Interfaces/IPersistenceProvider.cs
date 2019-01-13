using Common;
using System.Collections.Generic;
using WebApiServer.Models;

namespace Client.Providers.Interfaces
{
    public interface IPersistenceProvider
    {
        void SaveToken(AppSettings appSettings, string encryptedToken);

        void SetLanguage(string language);

        string GetLanguage();

        Jwt GetToken();

        IEnumerable<string> GetPermissionClaims();

        void SetCredentials(Models.Login model);

        void ClearCredentials();

        Models.Login GetCredentials();

        string GetEncryptedToken();

        void ClearToken();

    }
}