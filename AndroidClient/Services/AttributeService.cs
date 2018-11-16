
using Android.App;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AttributeService : Service
    {
        public AttributeService(
            Activity activity
            ) : base(activity, "api/attribute/")
        {
        }

        public async Task<List<string>> GetAttributeNames(string name)
        {
            var url = _url + "?name=" + name;

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<string>>(json);
            }

            return null;
        }
    }
}