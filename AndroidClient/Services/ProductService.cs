using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ProductService : Service
    {
        public ProductService() 
            : base("/api/product")
        {

        }

        public async Task<HttpResult<Models.Product>> GetProductByBarcode(string barcode, CancellationToken token = default(CancellationToken))
        {
            return await Post<Models.Product>(barcode, "/barcode", token);
        }

        public async Task<HttpResult<T>> Post<T>(string barcode, string path, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            _requestMessage.Content = new StringContent(barcode);
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<T>();
            result.Error = new Dictionary<string, string[]>();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var stream = await response.Content.ReadAsStreamAsync();
                    result.Data = DeserializeJsonFromStream<T>(stream);
                    break;
                case HttpStatusCode.NotFound:
                    result.Error = new Dictionary<string, string[]>();
                    result.Error.Add("Barcode", new[] { "Not found" });
                    break;
                default:
                    result.Error = new Dictionary<string, string[]>();
                    break;
            }

            CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> UpdateProduct(Models.Product entity, CancellationToken token = default(CancellationToken))
        {
            return await Post(entity, token);
        }

        public async Task<HttpResult<List<Models.Product>>> GetProducts(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            return await PostPaged<Models.Product>(criteria, token);
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
    }
}