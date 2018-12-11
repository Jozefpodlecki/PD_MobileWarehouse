
using Android.App;
using Common;
using Common.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ProductService : Service
    {
        public ProductService(Activity activity) : base(activity, "api/product/")
        {

        }

        public async Task<HttpResult<List<Models.Product>>> GetProducts(FilterCriteria criteria)
        {
            var url = _url + "search";

            var json = JsonConvert.SerializeObject(criteria);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var response = await _client.PostAsync(url, content);

            var result = new HttpResult<List<Models.Product>>();

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<List<Models.Product>>(json);
            }
            else
            {
                json = await response.Content.ReadAsStringAsync();
                result.Error = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
            }

            return result;
        }

        public async Task<List<string>> GetProductNames(string name)
        {
            var url = _url + "search?name=" + name;

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<string>>(json);
            }

            return null;
        }

        public async Task<bool> AddProduct(Product product)
        {

            return false;
        }
    }
}