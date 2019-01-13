using Client.Managers;
using Client.Providers;
using Client.Services.Interfaces;

namespace Client.Services
{
    public class ServicesProvider : IServicesProvider
    {
        public void LoadServices(MainActivity activity)
        {
            var httpClientManager = new HttpClientManager();
            activity.HttpClientManager = httpClientManager;
            var httpHelper = new HttpHelper();

            activity.PersistenceProvider = new PersistenceProvider(activity);
            activity.RoleManager = new RoleManager(activity.PersistenceProvider);
            activity.AttributeService = new AttributeService(httpClientManager, httpHelper, "/api/attribute");
            activity.AuthService = new AuthService(httpClientManager, httpHelper, "/api/auth");
            activity.CityService = new CityService(httpClientManager, httpHelper, "/api/city");
            activity.CounterpartyService = new CounterpartyService(httpClientManager, httpHelper, "/api/counterparty");
            activity.InvoiceService = new InvoiceService(httpClientManager, httpHelper, "/api/invoice");
            activity.HLocationService = new LocationService(httpClientManager, httpHelper, "/api/location");
            activity.NoteService = new NoteService(httpClientManager, httpHelper, "/api/note");
            activity.ProductService = new ProductService(httpClientManager, httpHelper, "/api/product");
            activity.RoleService = new RoleService(httpClientManager, httpHelper, "/api/role");
            activity.HUserService = new UserService(httpClientManager, httpHelper, "/api/user");
        }
    }
}