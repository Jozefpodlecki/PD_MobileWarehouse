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
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using System.Threading;
using Client.Providers;
using Client.Services;

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
        public AuthService AuthService;
        public LocationService HLocationService;
        public NoteService NoteService;
        public ProductService ProductService;
        public RoleService RoleService;
        public UserService HUserService;
        //public CameraProvider cameraProvider { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            Initialize();

            using (var cts = new CancellationTokenSource())
            {
                var appSettings = ConfigurationManager.Instance.GetAsync(cts.Token).Result;

                var tokenProvider = new TokenProvider(this, appSettings);

                Client.Services.Service.AppSettings = appSettings;
                Client.Services.Service.TokenProvider = tokenProvider;
            }

            AuthService = new AuthService(this);
            HLocationService = new LocationService(this);
            NoteService = new NoteService(this);
            ProductService = new ProductService(this);
            RoleService = new RoleService(this);
            HUserService = new UserService(this);
            
            NavigationManager = new NavigationManager(this);
            //NavigationManager.GoToCounterparties();
            //NavigationManager.GoToAddCounterparty();
            NavigationManager.GoToAddInvoice();
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

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            NavigationView = FindViewById<NavigationView>(Resource.Id.MainNavigationView);
            //BottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.BottomNavigationView);
            ActivityMainLayout = FindViewById<DrawerLayout>(Resource.Id.ActivityMainLayout);
            NavigationView.SetNavigationItemSelectedListener(this);
            
            

            SetSupportActionBar(toolbar);

            InitializeMenu();
        }

        public void InitializeMenu()
        {
            //BottomNavigationView.InflateMenu(Resource.Menu.BottomNavigation);
            //BottomNavigationView.SetOnNavigationItemSelectedListener(this);
            //BottomNavigationView.Visibility = ViewStates.Invisible;

            NavigationView.InflateHeaderView(Resource.Layout.NavigationHeader);
            NavigationView.InflateMenu(Resource.Menu.NavigationMenu);
            //NavigationView.Visibility = ViewStates.Invisible;

            //ActivityMainLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);

            /*
            var dict = new Dictionary<int, string>()
            {
                //{ Resource.Id.productsMenuItem, SiteClaimTypes.},
                { Resource.Id.attributesMenuItem, ""},
                { Resource.Id.usersMenuItem, ""},
                { Resource.Id.rolesMenuItem, ""}
            };

            NavigationView.Menu.FindItem(Resource.Id.productsMenuItem);
            NavigationView.Menu.FindItem(Resource.Id.attributesMenuItem);
            */
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
                    NavigationView.Visibility = ViewStates.Invisible;
                    ActivityMainLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
                    NavigationManager.GoToLogin();
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

    }
}

