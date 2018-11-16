using System;
using Android.Content;
using Client.Providers;

namespace Client.Factories
{
    public class AndroidConfigurationStreamProviderFactory : IConfigurationStreamProviderFactory
    {
        private readonly Func<Context> _contextProvider;

        public AndroidConfigurationStreamProviderFactory(Func<Context> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public IConfigurationStreamProvider Create()
        {
            return new AndroidConfigurationStreamProvider(_contextProvider);
        }
    }
}