using Client.DemoBackend;
using Client.Services.Interfaces;
using WebApiServer.Models;

namespace Client.Services.Mock
{
    public class MockAuthorizationManager : IAuthorizationManager
    {
        private readonly SQLiteConnectionManager _sqliteConnectionManager;
        private string _token;
        private Jwt _jwt;

        public MockAuthorizationManager(SQLiteConnectionManager sqliteConnectionManager)
        {
            _sqliteConnectionManager = sqliteConnectionManager;
        }

        public bool CheckAuthorization()
        {
            return true;
        }

        public void ClearAuthorization()
        {
            
        }

        public void SetAuthorization(object data)
        {
            var arr = (object[])data;

            _token = (string)arr[0];
            _jwt = (Jwt)arr[1];

            if(_jwt == null)
            {
                return;
            }

            var userId = int.Parse(_jwt.Id);

            _sqliteConnectionManager.SetUser(userId);
        }
    }
}