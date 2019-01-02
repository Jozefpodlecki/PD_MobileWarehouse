using Client.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Managers
{
    public class RoleManager
    {
        private PersistenceProvider _persistenceProvider;

        public RoleManager(PersistenceProvider persistenceProvider)
        {
            _persistenceProvider = persistenceProvider;
            Permissions = new Dictionary<int, string>();
            Claims = new Dictionary<string, string>();
        }

        public Dictionary<int,string> Permissions { get; set; }

        public Dictionary<string, string> Claims { get; set; }

        public void CalculatePermissions()
        {
            var token = _persistenceProvider.GetToken();

            Permissions = new Dictionary<int, string>(token.Claims
                .Join(Constants.MenuItemClaimMap, kv => kv, kv => kv.Value, (cl, clm) => clm));

            Claims = token
                .Claims
                .ToDictionary(kv => kv);
        }
    }
}