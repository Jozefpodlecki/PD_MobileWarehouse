﻿using Android.Content;
using Client.Providers.Interfaces;
using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApiServer.Models;

namespace Client.Providers
{
    public class PersistenceProvider : IPersistenceProvider
    {
        private Context _context;

        public PersistenceProvider(
            Context context)
        {
            _context = context;
        }

        public void SaveToken(AppSettings appSettings, string encryptedToken)
        {
            var secretKey = appSettings.JwtConfiguration.ByteKey;
            var jwt = JWT.JsonWebToken.DecodeToObject<Jwt>(encryptedToken, secretKey);
            var json = JsonConvert.SerializeObject(jwt);
            var preferences = _context.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var edit = preferences.Edit();

            edit
                .PutString(Constants.JWTResourceToken, encryptedToken)
                .PutString(Constants.JWTResourceJWT, json).Commit();
        }

        public void SetLanguage(string language)
        {
            var preferences = _context.GetSharedPreferences(Constants.ConfigResource, Android.Content.FileCreationMode.Private);
            var edit = preferences.Edit();
            edit.PutString(Constants.Language, language).Commit();
        }

        public string GetLanguage()
        {
            var preferences = _context.GetSharedPreferences(Constants.ConfigResource, Android.Content.FileCreationMode.Private);
            var language = preferences.GetString(Constants.Language, null);

            return language;
        }

        public Jwt GetToken()
        {
            var preferences = _context.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var json = preferences.GetString(Constants.JWTResourceJWT, null);

            if(json == null)
            {
                return null;
            }

            var jwt = JsonConvert.DeserializeObject<Jwt>(json);

            return jwt;
        }

        public IEnumerable<string> GetPermissionClaims()
        {
            var preferences = _context.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var json = preferences.GetString(Constants.JWTResourceJWT, null);

            if (json == null)
            {
                return null;
            }

            var jwt = JsonConvert.DeserializeObject<Jwt>(json);

            return jwt.Claims;
        }

        public void SetCredentials(Models.Login model)
        {
            var json = JsonConvert.SerializeObject(model);
            var preferences = _context.GetSharedPreferences(Constants.CredentialResource, Android.Content.FileCreationMode.Private);
            var edit = preferences.Edit();
            edit.PutString(Constants.Credential, json).Commit();
        }

        public void ClearCredentials()
        {
            var preferences = _context.GetSharedPreferences(Constants.CredentialResource, Android.Content.FileCreationMode.Private);
            var edit = preferences.Edit();
            edit.Remove(Constants.Credential).Commit();
        }

        public Models.Login GetCredentials()
        {
            var preferences = _context.GetSharedPreferences(Constants.CredentialResource, Android.Content.FileCreationMode.Private);
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
            var preferences = _context.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var token = preferences.GetString(Constants.JWTResourceToken, null);

            return token;
        }

        public void ClearToken()
        {
            var preferences = _context.GetSharedPreferences(Constants.JWTResource, Android.Content.FileCreationMode.Private);
            var edit = preferences.Edit();
            edit
                .Remove(Constants.JWTResourceToken)
                .Remove(Constants.JWTResourceJWT)
                .Commit();
        }
    }
}