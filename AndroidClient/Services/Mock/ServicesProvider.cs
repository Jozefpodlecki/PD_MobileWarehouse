using Client.DemoBackend;
using Client.Managers.Mock;
using Client.Providers.Mock;
using Client.Services.Interfaces;
using Common.Mappers;
using WebApiServer.Managers;

namespace Client.Services.Mock
{
    public class ServicesProvider : IServicesProvider
    {
        public void LoadServices(MainActivity activity)
        {
            try
            {
                var sqliteConnectionManager = new SQLiteConnectionManager();
                var httpClientManager = new Services.Mock.MockAuthorizationManager(sqliteConnectionManager);
                activity.AuthorizationManager = httpClientManager;

                var mapper = new Mapper();
                var passwordManager = new PasswordManager();
                var unitofWork = new UnitOfWork(sqliteConnectionManager, mapper, passwordManager);
                var migrator = new DemoMigrator(sqliteConnectionManager, passwordManager);
                migrator.Migrate();
                var jwtTokenProvider = new JwtTokenProvider(activity.AppSettings);
                activity.PersistenceProvider = new PersistenceProvider();
                activity.RoleManager = new RoleManager();
                activity.AttributeService = new Services.Mock.AttributeService(unitofWork);
                activity.AuthService = new Services.Mock.AuthService(unitofWork, jwtTokenProvider, passwordManager);
                activity.CityService = new Services.Mock.CityService(unitofWork);
                activity.CounterpartyService = new Services.Mock.CounterpartyService(unitofWork);
                activity.InvoiceService = new Services.Mock.InvoiceService(unitofWork);
                activity.HLocationService = new Services.Mock.LocationService(unitofWork);
                activity.NoteService = new Services.Mock.NoteService(unitofWork);
                activity.ProductService = new Services.Mock.ProductService(unitofWork);
                activity.RoleService = new Services.Mock.RoleService(unitofWork);
                activity.HUserService = new Services.Mock.UserService(unitofWork);
            }
            catch (System.Exception ex)
            {
               
            }

        }
    }
}