using System;
using System.IO;
using System.Threading.Tasks;

namespace Client.Providers
{
    public interface IConfigurationStreamProvider : IDisposable
    {
        Task<Stream> GetStreamAsync();
    }
}