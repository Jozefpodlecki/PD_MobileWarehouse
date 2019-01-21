using System;
using System.IO;
using Common.Repository.Interfaces;

namespace Client.DemoBackend
{
    public class SQLiteConnectionManager
    {
        public ISQLiteConnection Connection { get; set; }

        public SQLiteConnectionManager()
        {
            var databaseName = "MobileWarehouseDemo.db";
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);

            Connection = new SiteSQLiteConnection(databasePath);
        }

        public void SetUser(int userId)
        {
            Connection.UserId = userId;
        }
    }
}