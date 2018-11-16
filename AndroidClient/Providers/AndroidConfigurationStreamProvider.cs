using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using AndroidClient;

namespace Client.Providers
{
    public class AndroidConfigurationStreamProvider : IConfigurationStreamProvider
    {
        private const string ConfigurationFilePath = Constants.ConfigurationFilePath;

        private readonly Func<Context> _contextProvider;

        private Stream _readingStream;

        public AndroidConfigurationStreamProvider(Func<Context> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public Task<Stream> GetStreamAsync()
        {
            ReleaseUnmanagedResources();

            var assets = _contextProvider().Assets;

            _readingStream = assets.Open(ConfigurationFilePath);

            return Task.FromResult(_readingStream);
        }

        private void ReleaseUnmanagedResources()
        {
            _readingStream?.Dispose();
            _readingStream = null;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~AndroidConfigurationStreamProvider()
        {
            ReleaseUnmanagedResources();
        }
    }
}