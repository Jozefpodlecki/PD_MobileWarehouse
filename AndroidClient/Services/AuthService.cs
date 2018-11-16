using Android.App;
using Client.Models;
using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuthService : Service
    {
        public AuthService(
            Activity activity
            ) : base(activity,"api/auth/")
        {
        }
        
        public async Task<HttpResult<string>> Login(Login model)
        {
            var url = _url + "login";
    
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var response = await _client.PostAsync(url, content);

            var result = new HttpResult<string>();

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                json = await response.Content.ReadAsStringAsync();
                result.Error = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
            }

            return result;
        }
        
    }
}