using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Common;
using Common.DTO;
using Newtonsoft.Json;

namespace Client.Services
{
    public class RoleService : Service
    {
        public RoleService(
            Activity activity
            ) : base(activity, "api/role/")
        {
        }

        public async Task<List<Claim>> GetClaims()
        {
            var url = _url + "/claims";

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Claim>>(json);
            }

            return null;

        }

        public async Task<List<Role>> GetRoles(FilterCriteria criteria)
        {
            var url = _url + "/search";

            var json = JsonConvert.SerializeObject(criteria);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Role>>(json);
            }

            return Enumerable.Empty<Role>().ToList();
        }

        public async Task<List<string>> GetRoles(string name)
        {
            return null;
        }

        public async Task AddRole(Role role)
        {
            return;
        }

        public async Task EditRole(Role role)
        {
            return;
        }

        public async Task DeleteRole(Role role)
        {
            return;
        }
    }
}