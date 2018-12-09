using Common;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Managers.ConfigurationManager
{
    public interface IConfigurationManager
    {
        Task<AppSettings> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}