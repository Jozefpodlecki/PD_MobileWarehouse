
using Android.App;
using Common;
using Common.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class NoteService : Service
    {
        public NoteService(
            Activity activity
            ) : base(activity, "api/note/")
        {
        }

        public async Task<List<Counterparty>> GetCounterparties(FilterCriteria criteria)
        {
            var url = _url + "counterparties/search";

            var json = JsonConvert.SerializeObject(criteria);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Counterparty>>(json);
            }

            return null;
        }
    }
}