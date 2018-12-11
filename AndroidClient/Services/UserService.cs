using Android.App;
using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class UserService : Service
    {
        public UserService(Activity activity) : base(activity,"/api/user")
        {

        }

        public async Task<List<Models.User>> GetUsers(FilterCriteria criteria)
        {
            var url = _url + "/search";

            var json = JsonConvert.SerializeObject(criteria);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Models.User>>(json);
            }

            return Enumerable.Empty<Models.User>().ToList();
        }

        public async Task AddUser(Models.User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var response = await _client.PostAsync(_url, content);

            if (response.IsSuccessStatusCode)
            {
                
            }
        }
    }
}