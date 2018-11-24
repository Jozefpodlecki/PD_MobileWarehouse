
using Android.App;
using AndroidClient;
using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApiServer.Models;

namespace Client.Providers
{
    public class TokenProvider
    {
        private Activity _activity;
        private AppSettings _appSettings;

        public TokenProvider(
            Activity activity,
            AppSettings appSettings)
        {
            _activity = activity;
            _appSettings = appSettings;
        }

        public void SaveToken(string encryptedToken)
        {
            var secretKey = _appSettings.JwtConfiguration.ByteKey;
            var jwt = JWT.JsonWebToken.DecodeToObject<Jwt>(encryptedToken, secretKey);

            var json = JsonConvert.SerializeObject(jwt);
            var preferences = _activity.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var edit = preferences.Edit();
            edit.PutString(Constants.JWTResourceToken, encryptedToken).Commit();
            edit.PutString(Constants.JWTResourceJWT, json).Commit();
        }

        public Jwt GetToken()
        {
            var preferences = _activity.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var json = preferences.GetString(Constants.JWTResourceJWT, null);
            var jwt = JsonConvert.DeserializeObject<Jwt>(json);

            return jwt;
        }

        public List<string> GetPermissionClaims()
        {
            var preferences = _activity.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var json = preferences.GetString(Constants.JWTResourceJWT, null);
            var jwt = JsonConvert.DeserializeObject<Jwt>(json);

            return null;
        }

        public void SetCredentials(Models.Login model)
        {
            var json = JsonConvert.SerializeObject(model);
            var preferences = _activity.GetSharedPreferences(Constants.CredentialResource, Android.Content.FileCreationMode.Private);
            var edit = preferences.Edit();
            edit.PutString(Constants.Credential, json).Commit();
        }

        public void ClearCredentials()
        {
            var preferences = _activity.GetSharedPreferences(Constants.CredentialResource, Android.Content.FileCreationMode.Private);
            var edit = preferences.Edit();
            edit.Remove(Constants.Credential).Commit();
        }

        public Models.Login GetCredentials()
        {
            var preferences = _activity.GetSharedPreferences(Constants.CredentialResource, Android.Content.FileCreationMode.Private);
            var json = preferences.GetString(Constants.Credential, null);

            if(json == null)
            {
                return null;
            }

            var model = JsonConvert.DeserializeObject<Models.Login>(json);

            return model;
        }

        public string GetEncryptedToken()
        {
            var preferences = _activity.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var json = preferences.GetString(Constants.JWTResourceToken, null);

            if(json == null)
            {
                return json;
            }

            var token = JsonConvert.DeserializeObject<string>(json);

            return token;
        }

    }
}