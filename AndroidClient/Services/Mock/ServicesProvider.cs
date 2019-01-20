using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Managers.Mock;
using Client.Providers.Mock;
using Client.Services.Interfaces;
using Client.SQLite;

namespace Client.Services.Mock
{
    public class ServicesProvider : IServicesProvider
    {
        public void LoadServices(MainActivity activity)
        {
            try
            {
                var sqliteDbContext = new SQLiteDbContext();

                var httpClientManager = new Services.Mock.HttpClientManager();
                activity.HttpClientManager = httpClientManager;

                activity.PersistenceProvider = new PersistenceProvider();
                activity.RoleManager = new RoleManager();
                activity.AttributeService = new Services.Mock.AttributeService(sqliteDbContext);
                activity.AuthService = new Services.Mock.AuthService();
                activity.CityService = new Services.Mock.CityService(sqliteDbContext);
                activity.CounterpartyService = new Services.Mock.CounterpartyService(sqliteDbContext);
                activity.InvoiceService = new Services.Mock.InvoiceService(sqliteDbContext);
                activity.HLocationService = new Services.Mock.LocationService(sqliteDbContext);
                activity.NoteService = new Services.Mock.NoteService(sqliteDbContext);
                activity.ProductService = new Services.Mock.ProductService(sqliteDbContext);
                activity.RoleService = new Services.Mock.RoleService(sqliteDbContext);
                activity.HUserService = new Services.Mock.UserService(sqliteDbContext);
            }
            catch(Exception ex)
            {
                if(ex.InnerException != null)
                {
                    Toast.MakeText(activity, ex.InnerException.ToString(), ToastLength.Long).Show();
                }
                Toast.MakeText(activity, ex.ToString(), ToastLength.Long).Show();
                Toast.MakeText(activity, ex.StackTrace, ToastLength.Long).Show();
            }
            
        }
    }
}