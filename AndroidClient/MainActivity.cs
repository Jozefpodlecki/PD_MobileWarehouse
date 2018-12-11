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
        public AppSettings AppSettings;
        public AuthService AuthService;
        public LocationService HLocationService;
        public NoteService NoteService;
        public ProductService ProductService;
        public RoleService RoleService;
        public UserService HUserService;
        public PersistenceProvider PersistenceProvider;
        private Task _getConfigTask;
        //public CameraProvider cameraProvider { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            Initialize();
            InitializeMenu();
            //LockMenu();

            var task = Task.Run(async () =>
            {
                AppSettings = await ConfigurationManager.Instance.GetAsync();
                
                Client.Services.Service.AppSettings = AppSettings;
                Client.Services.Service.TokenProvider = PersistenceProvider;
                AuthService = new AuthService(this);
                HLocationService = new LocationService(this);
                NoteService = new NoteService(this);
                ProductService = new ProductService(this);
                RoleService = new RoleService(this);
                HUserService = new UserService(this);
                NavigationManager = new NavigationManager(this);

                RunOnUiThread(() =>
                {
                    //NavigationManager.GoToLogin();
                    NavigationManager.GoToCounterparties();
                });
                
            });
            
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

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }


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

        public void RestrictMenus()
        {
            var token = PersistenceProvider.GetToken();

            var readClaims = token
                .Claims
                .ToDictionary(kv => kv);


            foreach (var item in Constants.MenuItemClaimMap)
            {
                if (!readClaims.ContainsKey(item.Value))
                {
                    var menuItem = NavigationView.Menu.FindItem(item.Key) 
                        ?? Toolbar.Menu.FindItem(item.Key);

                    menuItem.SetVisible(false);
                }
            }
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
            //Toolbar.Menu.FindItem(Resource.Id.ScanBarcodeActionBarMenuItem).SetVisible(false);
            //Toolbar.Menu.FindItem(Resource.Id.ScanOCRActionBarMenuItem).SetVisible(false);

            return true;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
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
                    RestrictMenus();
                    NavigationManager.GoToLogin();
                break;
                case Resource.Id.LanguageActionBarMenuItem:
                    NavigationManager.GoToLanguages();
                break;
                case Resource.Id.ScanBarcodeActionBarMenuItem:
                    //var BARCODE_READER_ACTIVITY_REQUEST = 1208;
                    //var activity = BarcodeReaderActivity
                break;
                case Resource.Id.ScanOCRActionBarMenuItem:
                    
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

