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
        public static AppSettings AppSettings;
        public static PersistenceProvider TokenProvider;

        protected static HttpClient _client;
        protected static StringBuilder _stringBuilder;
        protected static Encoding _encoding;
        protected static JsonSerializer _jsonSerializer;
        protected static MediaTypeHeaderValue _jsonMediaTypeHeaderValue;
        protected static HttpRequestMessage _requestMessage;
        protected string _url;
        protected string _postFix;
        protected string _token;
        private JsonTextWriter _jsonTextWriter;
        private JsonTextReader _jsonTextReader;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private MemoryStream _memoryStream;
        
        static Service()
        {
            _jsonMediaTypeHeaderValue = new MediaTypeHeaderValue("application/json");
            _jsonSerializer = new JsonSerializer();
            _jsonSerializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            _client = new HttpClient();
            _stringBuilder = new StringBuilder(100);
            _encoding = new UTF8Encoding(false);
        }

        public Service(
            Activity activity,
            string postFix)
        {

            var url = AppSettings.Url;
            _postFix = postFix;
            _url = url + _postFix;
            
            //var header = new MediaTypeWithQualityHeaderValue("application/json");
            //_client.DefaultRequestHeaders.Accept.Add(header);
        }

        protected void SetAuthorizationHeader()
        {
            if(_client.DefaultRequestHeaders.Authorization == null)
            {
                return;
            }
            
            _token = TokenProvider.GetEncryptedToken();

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
            _streamReader = new StreamReader(stream);
            _jsonTextReader = new JsonTextReader(_streamReader);

            try
            {
                var searchResult = _jsonSerializer.Deserialize<T>(_jsonTextReader);
                return searchResult;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        protected void CleanUp()
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

        public async Task<HttpResult<List<T>>> Get<T>(string path = null, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}",_url, path);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<List<T>>();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var stream = await response.Content.ReadAsStreamAsync();
                    result.Data = DeserializeJsonFromStream<List<T>>(stream);
                    CleanUpReaders();
                    break;
                default:
                    result.Error = new Dictionary<string, string[]>();
                    break;
            }

            return result;
        }

        public async Task<HttpResult<List<T>>> Get<T>(string name, string value, string path = null, CancellationToken token = default(CancellationToken))
        {
            SetAuthorizationHeader();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0}{1}?{2}={3}", _url, path, name, value);

            var response = await _client.GetAsync(_stringBuilder.ToString(), token);

            var result = new HttpResult<List<T>>();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var stream = await response.Content.ReadAsStreamAsync();
                    result.Data = DeserializeJsonFromStream<List<T>>(stream);
                    CleanUpReaders();
                    break;
                default:
                    result.Error = new Dictionary<string, string[]>();
                    break;
            }

            return result;
        }

        public async Task<HttpResult<List<T>>> PostPaged<T>(FilterCriteria criteria, string path = null, CancellationToken token = default(CancellationToken))
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

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var stream = await response.Content.ReadAsStreamAsync();
                    result.Data = DeserializeJsonFromStream<List<T>>(stream);
                    CleanUpReaders();
                    break;
                default:
                    result.Error = new Dictionary<string, string[]>();
                    break;
            }

            CleanUp();
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

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result.Data = true;
                    break;
                case HttpStatusCode.NotFound:
                    result.Data = false;
                    break;
                default:
                    result.Error = new Dictionary<string, string[]>();
                    break;
            }

            CleanUp();
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

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result.Data = true;
                    break;
                case HttpStatusCode.NotFound:
                    result.Data = false;
                    break;
                default:
                    result.Error = new Dictionary<string, string[]>();
                    break;
            }

            CleanUp();
            return result;
        }

        public async Task<HttpResult<bool>> Delete(int id, string path = null, CancellationToken token = default(CancellationToken))
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

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result.Data = true;
                    break;
                case HttpStatusCode.NotFound:
                    result.Data = false;
                    break;
                default:
                    result.Error = new Dictionary<string, string[]>();
                    break;
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

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result.Data = true;
                    break;
                case HttpStatusCode.NotFound:
                    result.Data = false;
                    break;
                default:
                    result.Error = new Dictionary<string, string[]>();
                    break;
            }

            return result;
        }


    }
}