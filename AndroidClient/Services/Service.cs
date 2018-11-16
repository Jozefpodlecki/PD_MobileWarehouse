using Android.App;
using Client.Managers.ConfigurationManager;
using Client.Providers;
using Common;
using System.Net.Http;
using System.Threading;

namespace Client.Services
{
    public class Service
    {
        protected static HttpClient _client;
        private static readonly object padlock = new object();
        protected string _url;
        protected string _postFix;
        protected string _token;
        private AppSettings _appSettings;
        protected TokenProvider _tokenProvider;

        public Service(
            Activity activity,
            string postFix)
        {
            using (var cts = new CancellationTokenSource())
            {
                _appSettings = ConfigurationManager.Instance.GetAsync(cts.Token).Result;
                var url = _appSettings.Url;
                _postFix = postFix;
                _url = url + _postFix;
            }   

            _client = new HttpClient();

            _tokenProvider = new TokenProvider(activity, _appSettings);

            _token = _tokenProvider.GetEncryptedToken();
            
            if(_token != null)
            {
                var authHeader = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                _client.DefaultRequestHeaders.Authorization = authHeader;
            }

            var header = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(header);
        }


    }
}