using Android.App;
using Client.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuthService : Service
    {
        public AuthService(
            Activity activity
            ) : base(activity,"api/auth")
        {
        }
        
        public async Task<HttpResult<string>> Login(Login model, CancellationToken token = default(CancellationToken))
        {
            return await Post(model,"/login", token);
        }

        public async Task<HttpResult<string>> Post(Login model, string path = null, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = CreateJsonContent(model);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<string>();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result.Data = await response.Content.ReadAsStringAsync();
                    break;
                case HttpStatusCode.NotFound:
                    result.Data = await response.Content.ReadAsStringAsync();
                    break;
                default:
                    var stream = await response.Content.ReadAsStreamAsync();
                    result.Error = DeserializeJsonFromStream<Dictionary<string, string[]>>(stream);

                    if (result.Error == null)
                    {
                        result.Error = new Dictionary<string, string[]>();
                    }
                    
                    break;
            }

            CleanUp();
            return result;
        }
    }
}