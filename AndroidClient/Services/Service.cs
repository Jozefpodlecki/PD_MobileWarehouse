using Android.App;
using Client.Managers.ConfigurationManager;
using Client.Providers;
using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiServer.Models;

namespace Client.Services
{

    public class Service
    {
        private HttpClient _client;
        private StringBuilder _stringBuilder;
        private HttpRequestMessage _requestMessage;
        private string _postFix;
        private string _url => _httpClientManager.BaseUrl + _postFix;

        private readonly HttpClientAuthorizationManager _httpClientManager;
        private readonly HttpHelper _httpHelper;

        public Service(
            HttpClientAuthorizationManager httpClientManager,
            HttpHelper httpHelper,
            string postFix)
        {
            _httpClientManager = httpClientManager;
            _httpHelper = httpHelper;
            _client = httpClientManager.HttpClient;
            _stringBuilder = new StringBuilder(100);
            _postFix = postFix;
        }

        public async Task FillErrors(HttpResponseMessage response, Dictionary<string, string[]> error)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden:
                    error.Add("Server", new[] { nameof(HttpStatusCode.Forbidden) });
                    break;
                case HttpStatusCode.Unauthorized:
                    error.Add("Server", new[] { nameof(HttpStatusCode.Unauthorized) });
                    break;
                case HttpStatusCode.NotFound:
                    error.Add("Server", new[] { nameof(HttpStatusCode.NotFound) });
                    break;
                case HttpStatusCode.BadRequest:
                    error.Add("Server", new[] { nameof(HttpStatusCode.BadRequest) });
                    var errors = _httpHelper.DeserializeJsonFromStream<Dictionary<string, string[]>>(await response.Content.ReadAsStreamAsync());
                    if(errors != null)
                    {
                        foreach (var item in errors)
                        {
                            error.Add(item.Key, item.Value);
                        }
                    }
                    break;
                case HttpStatusCode.BadGateway:
                    error.Add("Server", new[] { nameof(HttpStatusCode.BadGateway) });
                    break;
                case HttpStatusCode.InternalServerError:
                    var content = await response.Content.ReadAsStringAsync();
                    error.Add("Server", new[] { nameof(HttpStatusCode.InternalServerError), content });
                    break;

            }
            
        }

        public async Task<HttpResult<T>> Get<T>(int id, string path = null, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}/{2}", _url, path, id);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<T>();
            await FillErrors(response, result.Error);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = _httpHelper.DeserializeJsonFromStream<T>(stream);
                _httpHelper.CleanUpReaders();
            }

            return result;
        }

        public async Task<HttpResult<T>> Get<T>(int id, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}/{1}", _url, id);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<T>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = _httpHelper.DeserializeJsonFromStream<T>(stream);
                _httpHelper.CleanUpReaders();
            }

            return result;
        }

        public async Task<HttpResult<List<T>>> Get<T>(string path = null, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}",_url, path);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<List<T>>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = _httpHelper.DeserializeJsonFromStream<List<T>>(stream);
                _httpHelper.CleanUpReaders();
            }

            return result;
        }

        public async Task<HttpResult<List<T>>> Get<T>(string name, string value, string path, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}?{2}={3}", _url, path, name, value);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<List<T>>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = _httpHelper.DeserializeJsonFromStream<List<T>>(stream);
                _httpHelper.CleanUpReaders();
            }

            return result;
        }

        public async Task<HttpResult<List<T>>> PostPaged<T>(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, "/search");

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = _httpHelper.CreateJsonContent(criteria);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<List<T>>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = _httpHelper.DeserializeJsonFromStream<List<T>>(stream);

                if(result.Data == null)
                {
                    result.Data = new List<T>();
                }

                _httpHelper.CleanUpReaders();
            }

            _httpHelper.CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<List<T>>> PostPaged<T>(FilterCriteria criteria, string path, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}{2}",_url, path, "/search");

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = _httpHelper.CreateJsonContent(criteria);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<List<T>>();

            await FillErrors(response, result.Error);

            token.ThrowIfCancellationRequested();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = _httpHelper.DeserializeJsonFromStream<List<T>>(stream);
                _httpHelper.CleanUpReaders();
            }

            _httpHelper.CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Put<T>(T dto, string path = null, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put
            };

            var content = _httpHelper.CreateJsonContent(dto);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<bool>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.Data = true;
            }
            else
            {
                result.Data = false;
            }

            _httpHelper.CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Put<T>(T dto, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put
            };

            var content = _httpHelper.CreateJsonContent(dto);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_url);
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<bool>();
            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.Data = true;
            }
            else
            {
                result.Data = false;
            }

            _httpHelper.CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Post<T>(T dto, string path = null, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = _httpHelper.CreateJsonContent(dto);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<bool>();
            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.Data = true;
            }
            else
            {
                result.Data = false;
            }

            _httpHelper.CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<T>> Post<T>(string data, string path, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            _requestMessage.Content = new StringContent(data);
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<T>();
            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = _httpHelper.DeserializeJsonFromStream<T>(stream);

                _httpHelper.CleanUpReaders();
            }

            _httpHelper.CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<string>> PostString<T>(T dto, string path = null, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = _httpHelper.CreateJsonContent(dto);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<string>();
            await FillErrors(response, result.Error);

            result.Data = await response.Content.ReadAsStringAsync();

            _httpHelper.CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Post<T>(T dto, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = _httpHelper.CreateJsonContent(dto);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_url);
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<bool>();
            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.Data = true;
            }
            else
            {
                result.Data = false;
            }

            _httpHelper.CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Delete(int id, string path, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}/{2}", _url, path, id);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete
            };
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            var result = new HttpResult<bool>();
            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.Data = true;
            }
            else
            {
                result.Data = false;
            }

            return result;
        }

        public async Task<HttpResult<bool>> Delete(int id, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}/{1}", _url, id);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete
            };
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            var result = new HttpResult<bool>();
            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.Data = true;
            }
            else
            {
                result.Data = false;
            }

            return result;
        }

        public async Task<HttpResult<bool>> Exists(string name, string value, string path = null, CancellationToken token = default(CancellationToken))
        {
            _httpClientManager.CheckAuthorization();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}?{2}={3}", _url, path, name, value);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Head
            };
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());

            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<bool>();
            await FillErrors(response, result.Error);

            token.ThrowIfCancellationRequested();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.Data = true;
            }
            else
            {
                result.Data = false;
            }

            token.ThrowIfCancellationRequested();

            return result;
        }


    }
}