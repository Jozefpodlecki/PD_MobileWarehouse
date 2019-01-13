using Client.Managers.Interfaces;
using Common;
using System.Collections.Generic;
using System.Linq;

namespace Client.Managers.Mock
{
    public class RoleManager : IRoleManager
    {
        private Dictionary<int, string> _permissions;
        private Dictionary<string, string> _claims;

        Dictionary<int, string> IRoleManager.Permissions => _permissions;
        Dictionary<string, string> IRoleManager.Claims => _claims;

        public RoleManager()
        {
            
        }

        void IRoleManager.CalculatePermissions()
        {
            _permissions = Constants.MenuItemClaimMap;

            _claims = SiteClaimValues
                .ClaimValues
                .ToDictionary(kv => kv, kv => kv);
        }
    }
}