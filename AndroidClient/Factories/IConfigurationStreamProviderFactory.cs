using Client.Providers;

namespace Client.Factories
{
    public interface IConfigurationStreamProviderFactory
    {
        IConfigurationStreamProvider Create();
    }
}