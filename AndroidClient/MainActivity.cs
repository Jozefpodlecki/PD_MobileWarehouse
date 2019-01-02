using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Client.Managers.ConfigurationManager;
using Client.Factories;
using Client.Managers;
using System.Threading.Tasks;
using Android.Content;
using Android.Runtime;
using System.Threading;
using Client.Providers;
using Client.Services;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using System.Collections.Generic;
using Common;
using System.Linq;
using Android.Content.Res;
using Java.Util;

namespace Client
{
    [Activity(Label = "@string/ApplicationName",
        Theme = "@style/AppTheme.NoActionBar",
        MainLauncher = true)]
	public class MainActivity : AppCompatActivity,
        BottomNavigationView.IOnNavigationItemSelectedListener,
        NavigationView.IOnNavigationItemSelectedListener
    {
        public NavigationView NavigationView { get; set; }
        public NavigationManager NavigationManager { get; set; }
        public BottomNavigationView BottomNavigationView { get; set; }
        public DrawerLayout ActivityMainLayout { get; set; }
        public Android.Support.V7.Widget.Toolbar Toolbar { get; set; }
        public IMenuItem CurrentMenuItem { get; private set; }

        public AppSettings AppSettings;
        public AttributeService AttributeService;
        public AuthService AuthService;
        public CityService CityService;
        public CounterpartyService CounterpartyService;
        public InvoiceService InvoiceService;
        public LocationService HLocationService;
        public NoteService NoteService;
        public ProductService ProductService;
        public RoleService RoleService;
        public UserService HUserService;
        public PersistenceProvider PersistenceProvider;
        public RoleManager RoleManager;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            Initialize();
            InitializeMenu();

            var task = Task.Run(async () =>
            {
                AppSettings = await ConfigurationManager.Instance.GetAsync();

                Client.Services.Service.PersistenceProvider = PersistenceProvider;
                AttributeService = new AttributeService();
                AuthService = new AuthService();
                CityService = new CityService();
                CounterpartyService = new CounterpartyService();
                InvoiceService = new InvoiceService();
                HLocationService = new LocationService();
                NoteService = new NoteService();
                ProductService = new ProductService();
                RoleService = new RoleService();
                HUserService = new UserService();
                NavigationManager = new NavigationManager(this);
                RoleManager = new RoleManager(PersistenceProvider);

                var loginModel = PersistenceProvider.GetCredentials();

                if(loginModel == null)
                {
                    loginModel = new Models.Login();

#if RELEASE
                    loginModel.ServerName = "http://192.168.1.35/WebApiServer/api/values";
#endif
#if DEBUG
                    loginModel.ServerName = "http://10.0.2.2/WebApiServer";
                    loginModel.Username = "admin1";
                    loginModel.Password = "123";
#endif

                    PersistenceProvider.SetCredentials(loginModel);
                }

                RunOnUiThread(() =>
                {
                    var token = PersistenceProvider.GetToken();

                    if (AuthenticateValidateToken(token))
                    {
                        Services.Service.BaseUrl = loginModel.ServerName;
                        OnLogin();   
                    }
                    else
                    {
                        LockMenu();
                        NavigationManager.GoToLogin();
                    }
                });
                
            });
            
        }

        public bool AuthenticateValidateToken(WebApiServer.Models.Jwt token = null)
        {
            token = token ?? PersistenceProvider.GetToken();

            if(token == null)
            {
                return false;
            }

            var expirationTime = DateTimeOffset
                .FromUnixTimeSeconds(int.Parse(token.ExpirationTime))
                .UtcDateTime;

            var currentUtcDate = DateTime.UtcNow;

            return currentUtcDate < expirationTime;
        }

        public override void OnBackPressed()
        {
            if (ActivityMainLayout.IsDrawerOpen(GravityCompat.Start))
            {
                ActivityMainLayout.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        protected override void OnPause() => base.OnPause();

        protected override void OnStop() => base.OnStop();


        private void Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            ConfigurationManager.Initialize(new AndroidConfigurationStreamProviderFactory(() => this));

            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            NavigationView = FindViewById<NavigationView>(Resource.Id.MainNavigationView);
            ActivityMainLayout = FindViewById<DrawerLayout>(Resource.Id.ActivityMainLayout);
            NavigationView.SetNavigationItemSelectedListener(this);
            SetSupportActionBar(Toolbar);

            var appName = Resources.GetString(Resource.String.ApplicationName);
            Toolbar.Title = appName;

        }

        public void InitializeMenu()
        {
            
            NavigationView.InflateHeaderView(Resource.Layout.NavigationHeader);
            NavigationView.InflateMenu(Resource.Menu.NavigationMenu);            
        }

        public void LockMenu()
        {
            NavigationView.Visibility = ViewStates.Invisible;
            ActivityMainLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
        }

        public void UnlockMenu()
        {
            NavigationView.Visibility = ViewStates.Visible;
            ActivityMainLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
        }

        public void OnLogin()
        {
            RoleManager.CalculatePermissions();

            UnlockMenu();
            RestrictMenus();

            //NavigationManager.GoToAccount();
            //NavigationManager.GoToAddGoodsReceivedNote();
            NavigationManager.GoToProducts();
        }

        public IEnumerable<IMenuItem> GetMenuItems()
        {
            var menus = new[] { NavigationView.Menu, Toolbar.Menu };
            foreach (var menu in menus)
            {
                var size = menu.Size();
                for (var i = 0; i < size; i++)
                {
                    yield return menu.GetItem(i);
                }
            }
        }

        public void RestrictMenus()
        {
            Task.Run(() =>
            {
                foreach (var menuItem in GetMenuItems())
                {
                    var claim = Constants.MenuItemClaimMap[menuItem.ItemId];
                    var visibility = RoleManager.Claims.ContainsKey(claim);

                    RunOnUiThread(() =>
                    {
                        menuItem.SetVisible(visibility);
                    });
                }
            });   
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);

            return true;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if(CurrentMenuItem != null 
                && item.ItemId == CurrentMenuItem.ItemId)
            {
                ActivityMainLayout.CloseDrawer(GravityCompat.Start);
                return true;
            }

            CurrentMenuItem = item;

            switch (item.ItemId)
            {
                case Resource.Id.AccountMenuItem:
                    NavigationManager.GoToAccount();
                break;
                case Resource.Id.UsersMenuItem:
                    NavigationManager.GoToUsers();
                break;
                case Resource.Id.RolesMenuItem:
                    NavigationManager.GoToRoles();
                break;
                case Resource.Id.AttributesMenuItem:
                    NavigationManager.GoToAttributes();
                    break;
                case Resource.Id.ProductsMenuItem:
                    NavigationManager.GoToProducts();
                break;
                case Resource.Id.InvoicesMenuItem:
                    NavigationManager.GoToInvoices();
                break;
                case Resource.Id.LocationsMenuItem:
                    NavigationManager.GoToLocations();
                break;
                case Resource.Id.CounterpartiesMenuItem:
                    NavigationManager.GoToCounterparties();
                break;
                case Resource.Id.GoodsReceivedNoteMenuItem:
                    NavigationManager.GoToGoodsReceivedNotes();
                break;
                case Resource.Id.GoodsDispatchedNoteMenuItem:
                    NavigationManager.GoToGoodsDispatchedNotes();
                break;
                case Resource.Id.LogoutMenuItem:
                    Services.Service.Logout();
                    PersistenceProvider.ClearToken();
                    LockMenu();
                    NavigationManager.GoToLogin();
                break;
                case Resource.Id.LanguageActionBarMenuItem:
                    NavigationManager.GoToLanguages();
                break;
                case Resource.Id.ScanBarcodeActionBarMenuItem:
                    NavigationManager.GoToBarcodeScanner();
                break;
                case Resource.Id.ScanOCRActionBarMenuItem:
                    NavigationManager.GoToQRScanner();
                break;
            }

            ActivityMainLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }

        bool BottomNavigationView.IOnNavigationItemSelectedListener.OnNavigationItemSelected(IMenuItem item) => OnNavigationItemSelected(item);
        bool NavigationView.IOnNavigationItemSelectedListener.OnNavigationItemSelected(IMenuItem item) => OnNavigationItemSelected(item);
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            OnNavigationItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void AttachBaseContext(Context context)
        {
            PersistenceProvider = new PersistenceProvider(context);

            base.AttachBaseContext(UpdateBaseContext(context));
        }

        private Context UpdateBaseContext(Context context)
        {
            var language = PersistenceProvider.GetLanguage();

            if (language == null)
            {
                return context;
            }

            var locale = new Locale(language);
            Locale.Default = locale;
            
            var configuration = context.Resources.Configuration;

            configuration.SetLocale(locale);
            context = context.CreateConfigurationContext(configuration);

            return context;
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }
    }
}

