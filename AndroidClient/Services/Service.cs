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

namespace Client.Services
{

    public class Service
    {
        public static PersistenceProvider PersistenceProvider;
        protected static HttpClient _client;
        protected StringBuilder _stringBuilder;
        protected static Encoding _encoding;
        protected static JsonSerializer _jsonSerializer;
        protected static MediaTypeHeaderValue _jsonMediaTypeHeaderValue;
        protected HttpRequestMessage _requestMessage;
        protected string _postFix;
        protected string _token;
        private JsonTextWriter _jsonTextWriter;
        private JsonTextReader _jsonTextReader;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private MemoryStream _memoryStream;
        public static string BaseUrl { get; set; }
        protected string _url => BaseUrl + _postFix;

        static Service()
        {
            _jsonMediaTypeHeaderValue = new MediaTypeHeaderValue("application/json");
            _jsonSerializer = new JsonSerializer();
            _jsonSerializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            _client = new HttpClient();
            _encoding = new UTF8Encoding();
        }

        public Service(string postFix)
        {
            _stringBuilder = new StringBuilder(100);
            _postFix = postFix;
        }

        protected void SetAuthorizationHeader()
        {
            if(_client.DefaultRequestHeaders.Authorization != null)
            {
                return;
            }
            
            _token = PersistenceProvider.GetEncryptedToken();

            if (_token == null)
            {
                return;
            }

            var authHeader = new AuthenticationHeaderValue("Bearer", _token);
            _client.DefaultRequestHeaders.Authorization = authHeader;
        }

        protected HttpContent CreateJsonContent(object content)
        {
            HttpContent httpContent = null;

            _memoryStream = new MemoryStream();
            _streamWriter = new StreamWriter(_memoryStream, _encoding, 1024, true);
            _jsonTextWriter = new JsonTextWriter(_streamWriter) { Formatting = Formatting.None };
            _jsonSerializer.Serialize(_jsonTextWriter, content);
            _jsonTextWriter.Flush();
            _memoryStream.Seek(0, SeekOrigin.Begin);
            httpContent = new StreamContent(_memoryStream);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            return httpContent;
        }

        protected T DeserializeJsonFromStream<T>(Stream stream)
        {
            _streamReader = new StreamReader(stream, _encoding, true, 1024);
            _jsonTextReader = new JsonTextReader(_streamReader);

            try
            {
                var obj = _jsonSerializer.Deserialize<T>(_jsonTextReader);
                return obj;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        protected void CleanUpWriters()
        {
            _jsonTextWriter.Close();
            _streamWriter.Close();
            _memoryStream.Close();
        }

        protected void CleanUpReaders()
        {
            _streamReader.Close();
            _jsonTextReader.Close();
        }

        public async Task FillErrors(HttpResponseMessage response, Dictionary<string, string[]> error)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    error.Add("Server", new[] { "Not found" });
                    break;
                case HttpStatusCode.BadRequest:
                    error.Add("Server", new[] { "Bad request" });
                    var errors = DeserializeJsonFromStream<Dictionary<string, string[]>>(await response.Content.ReadAsStreamAsync());
                    if(errors != null)
                    {
                        foreach (var item in errors)
                        {
                            error.Add(item.Key, item.Value);
                        }
                    }
                    break;
                case HttpStatusCode.BadGateway:
                    error.Add("Server", new[] { "Bad gateway" });
                    break;
                case HttpStatusCode.InternalServerError:
                    var content = await response.Content.ReadAsStringAsync();
                    error.Add("Server", new[] { "Internal server error", content });
                    break;

            }
            
        }

        public async Task<HttpResult<T>> Get<T>(int id, string path = null, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}/{2}", _url, path, id);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<T>();
            await FillErrors(response, result.Error);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = DeserializeJsonFromStream<T>(stream);
                CleanUpReaders();
            }

            return result;
        }

        public async Task<HttpResult<T>> Get<T>(int id, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}/{1}", _url, id);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<T>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = DeserializeJsonFromStream<T>(stream);
                CleanUpReaders();
            }

            return result;
        }

        public async Task<HttpResult<List<T>>> Get<T>(string path = null, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}",_url, path);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<List<T>>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = DeserializeJsonFromStream<List<T>>(stream);
                CleanUpReaders();
            }

            return result;
        }

        public async Task<HttpResult<List<T>>> Get<T>(string name, string value, string path, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}?{2}={3}", _url, path, name, value);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<List<T>>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = DeserializeJsonFromStream<List<T>>(stream);
                CleanUpReaders();
            }

            return result;
        }

        public async Task<HttpResult<List<T>>> PostPaged<T>(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, "/search");

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = CreateJsonContent(criteria);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<List<T>>();

            await FillErrors(response, result.Error);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                result.Data = DeserializeJsonFromStream<List<T>>(stream);

                if(result.Data == null)
                {
                    result.Data = new List<T>();
                }

                CleanUpReaders();
            }

            CleanUpWriters();
            return result;
        }

        public static void Logout()
        {
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<HttpResult<List<T>>> PostPaged<T>(FilterCriteria criteria, string path, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}{2}",_url, path, "/search");

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = CreateJsonContent(criteria);
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
                result.Data = DeserializeJsonFromStream<List<T>>(stream);
                CleanUpReaders();
            }

            CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Put<T>(T dto, string path = null, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put
            };

            var content = CreateJsonContent(dto);
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

            CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Put<T>(T dto, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put
            };

            var content = CreateJsonContent(dto);
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

            CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Post<T>(T dto, string path = null, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = CreateJsonContent(dto);
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

            CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<string>> PostString<T>(T dto, string path = null, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}", _url, path);

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = CreateJsonContent(dto);
            _requestMessage.Content = content;
            _requestMessage.RequestUri = new Uri(_stringBuilder.ToString());
            var response = await _client.SendAsync(_requestMessage, token);

            token.ThrowIfCancellationRequested();

            var result = new HttpResult<string>();
            await FillErrors(response, result.Error);

            result.Data = await response.Content.ReadAsStringAsync();

            CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Post<T>(T dto, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            token.ThrowIfCancellationRequested();

            _requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var content = CreateJsonContent(dto);
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

            CleanUpWriters();
            return result;
        }

        public async Task<HttpResult<bool>> Delete(int id, string path, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

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
            SetAuthorizationHeader();

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
            SetAuthorizationHeader();

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