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
using System.Collections.Generic;
using Android.Content.Res;
using Java.Util;
using Client.Services.Interfaces;
using Client.Listeners;
using Client.Providers.Interfaces;
using Client.Managers.Interfaces;
using System.Linq;
using Android.Widget;
using Common;
using Android.Content.PM;
using Client.Logger;
using System.Globalization;

namespace Client
{
    [Activity(Label = "@string/ApplicationName",
        Theme = "@style/AppTheme.NoActionBar",
        MainLauncher = true)]
	public class MainActivity : AppCompatActivity,
        NavigationView.IOnNavigationItemSelectedListener,
        IOnServerProvidedListener,
        IOnLoginListener
    {
        public NavigationView NavigationView { get; set; }
        public NavigationManager NavigationManager { get; set; }
        public DrawerLayout ActivityMainLayout { get; set; }
        public Android.Support.V7.Widget.Toolbar Toolbar { get; set; }
        public IMenuItem CurrentMenuItem { get; private set; }
        public System.Globalization.Calendar Calendar { get; set; }
        public FileLogger FileLogger { get; set; }

        public AppSettings AppSettings;
        public IAttributeService AttributeService;
        public IAuthService AuthService;
        public ICityService CityService;
        public ICounterpartyService CounterpartyService;
        public IInvoiceService InvoiceService;
        public ILocationService HLocationService;
        public INoteService NoteService;
        public IProductService ProductService;
        public IRoleService RoleService;
        public IUserService HUserService;
        public IPersistenceProvider PersistenceProvider;
        public IRoleManager RoleManager;
        public IAuthorizationManager AuthorizationManager;
        public Dictionary<int, Action> NaviagtionMenuMap;

        public void GoToFirstAvailableLocation()
        {
            var menuItemClaimMap = RoleManager
                .Permissions
                .FirstOrDefault(kv => kv.Value.Contains("Read"));

            NaviagtionMenuMap[menuItemClaimMap.Key]();
        }

        public void OnLogin()
        {
            UnlockMenu();
            RestrictMenus();
            NavigationManager.GoToAccount();
            //GoToFirstAvailableLocation();
        }

        public void OnLogin(Models.Login loginModel, string result)
        {
            PersistenceProvider.SaveToken(AppSettings, result);
            var data = new object[]
            {
                PersistenceProvider.GetEncryptedToken(),
                PersistenceProvider.GetToken(),
                loginModel.ServerName
            };
            AuthorizationManager.SetAuthorization(data);
            RoleManager.CalculatePermissions();

            OnLogin();
        }

        public void OnServerProvided(string serverName)
        {
            var environment = GetEnvironment();

            if (serverName == Constants.Localhost)
            {
                new Services.Mock.ServicesProvider().LoadServices(this);
            }
            else
            {
                new Services.ServicesProvider().LoadServices(this);
            }
        }

        public void SetLanguage(string language)
        {
            var preferences = ApplicationContext.GetSharedPreferences(Constants.ConfigResource, Android.Content.FileCreationMode.Private);
            preferences.Edit().PutString(Constants.Language, language).Commit();
        }

        public string GetLanguage(Context context)
        {
            var preferences = context.GetSharedPreferences(Constants.ConfigResource, Android.Content.FileCreationMode.Private);
            return preferences.GetString(Constants.Language, null);
        }

        public void SetEnvironment(string environment)
        {
            var preferences = GetSharedPreferences(Constants.ConfigResource, FileCreationMode.Private);
            var edit = preferences.Edit();
            edit
                .PutString(Constants.Environment, environment)
                .Commit();
        }

        public string GetEnvironment()
        {
            var preferences = GetSharedPreferences(Constants.ConfigResource, FileCreationMode.Private);
            return preferences.GetString(Constants.Environment, Constants.Demo);
        }

        public bool CheckAndRequestPermissions(IEnumerable<string> permissionNames)
        {
            var missingPermissions = permissionNames
                .Where(pn => CheckSelfPermission(pn) == Permission.Denied)
                .ToArray();

            if (missingPermissions.Any())
            {
                RequestPermissions(missingPermissions, 0);

                return false;
            }

            return true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
		{
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            Initialize();
            InitializeMenu();

            if (!CheckAndRequestPermissions(Constants.ApplicationPermissions))
            {
                return;
            }

            CreateLogger();

            Load();
        }

        public void CreateLogger()
        {
            try
            {
                FileLogger = new FileLogger();
            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, ex.ToString(), ToastLength.Long).Show();
            }
        }

        public void Load()
        {
            var environment = GetEnvironment();

            var task = Task.Run(async () =>
            {
                try
                {
                    AppSettings = await ConfigurationManager.Instance.GetAsync();
                    NavigationManager = new NavigationManager(this);
                    InitializeNaviagtionMenuMap();
                    Calendar = new System.Globalization.GregorianCalendar();

                    if (environment == Constants.Production)
                    {
                        new Services.ServicesProvider().LoadServices(this);
                        var loginModel = PersistenceProvider.GetCredentials();

                        var data = new object[]
                        {
                            PersistenceProvider.GetEncryptedToken(),
                            PersistenceProvider.GetToken(),
                            loginModel.ServerName
                        };
                        AuthorizationManager.SetAuthorization(data);

                        var validated = AuthorizationManager.CheckAuthorization();

                        if (validated)
                        {
                            OnLogin();
                        }
                        else
                        {
                            PersistenceProvider.ClearToken();
                            LockMenu();
                            NavigationManager.GoToLogin();
                        }
                    }
                    else
                    {
                        new Services.Mock.ServicesProvider().LoadServices(this);

                        RunOnUiThread(() =>
                        {
                            PersistenceProvider.ClearToken();
                            LockMenu();
                            NavigationManager.GoToLogin();
                        });
                    }
                }
                catch (Exception ex)
                {
                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                    });

                }


            });
        }

        public void InitializeNaviagtionMenuMap()
        {
            NaviagtionMenuMap = new Dictionary<int, Action>
            {
                { Resource.Id.AccountMenuItem, NavigationManager.GoToAccount},
                { Resource.Id.UsersMenuItem, NavigationManager.GoToUsers},
                { Resource.Id.RolesMenuItem, NavigationManager.GoToRoles},
                { Resource.Id.AttributesMenuItem, NavigationManager.GoToAttributes},
                { Resource.Id.ProductsMenuItem, NavigationManager.GoToProducts},
                { Resource.Id.InvoicesMenuItem, NavigationManager.GoToInvoices},
                { Resource.Id.LocationsMenuItem, NavigationManager.GoToLocations},
                { Resource.Id.CounterpartiesMenuItem, NavigationManager.GoToCounterparties},
                { Resource.Id.GoodsReceivedNoteMenuItem, NavigationManager.GoToGoodsReceivedNotes},
                { Resource.Id.GoodsDispatchedNoteMenuItem, NavigationManager.GoToGoodsDispatchedNotes},
                { Resource.Id.LogoutMenuItem, Logout},
                { Resource.Id.LanguageActionBarMenuItem, NavigationManager.GoToLanguages},
                { Resource.Id.ScanBarcodeActionBarMenuItem, () => { NavigationManager.GoToBarcodeScanner(); }},
                { Resource.Id.ScanOCRActionBarMenuItem, () => { NavigationManager.GoToQRScanner(); } }
            };
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
                    try
                    {
                        if(!Constants.MenuItemClaimMap.TryGetValue(menuItem.ItemId, out string claim))
                        {
                            continue;
                        }

                        var visibility = RoleManager.Claims.ContainsKey(claim);

                        RunOnUiThread(() =>
                        {
                            try
                            {
                                menuItem.SetVisible(visibility);
                            }
                            catch (Exception ex)
                            {

                            }

                        });
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    
                }
            });   
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            RunOnUiThread(() =>
            {
                Toast.MakeText(this, e.Exception.ToString(), ToastLength.Long).Show();
            });
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            RunOnUiThread(() =>
            {
                Toast.MakeText(this, e.ExceptionObject.ToString(), ToastLength.Long).Show();
            });
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);

            return true;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if(CurrentMenuItem != null 
                && item.ItemId == CurrentMenuItem.ItemId
                && item.ItemId != Resource.Id.LogoutMenuItem)
            {
                ActivityMainLayout.CloseDrawer(GravityCompat.Start);
                return true;
            }

            CurrentMenuItem = item;

            NaviagtionMenuMap[item.ItemId]();

            ActivityMainLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public void Logout()
        {
            AuthorizationManager.ClearAuthorization();
            PersistenceProvider.ClearToken();
            LockMenu();
            NavigationManager.GoToLogin();
        }

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
            base.AttachBaseContext(UpdateBaseContext(context));
        }

        private Context UpdateBaseContext(Context context)
        {
            var language = GetLanguage(context);

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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            CreateLogger();
            Load();
        }
    }
}