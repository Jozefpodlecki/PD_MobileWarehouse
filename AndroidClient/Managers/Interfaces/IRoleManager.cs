using System.Collections.Generic;

namespace Client.Managers.Interfaces
{
    public interface IRoleManager
    {
        Dictionary<int, string> Permissions { get; }

        Dictionary<string, string> Claims { get; }

        void CalculatePermissions();
    }
}