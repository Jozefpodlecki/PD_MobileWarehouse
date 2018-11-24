using Client;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using static Android.Views.View;
using Client.Services;
using static Android.Widget.AdapterView;
using Client.Managers;
using Android.Text;
using Java.Lang;
using System.Linq;
using Android.Support.V4.Widget;
using Client.Fragments;
using Client.Providers;
using System.Threading;
using Client.Managers.ConfigurationManager;
using Common;

namespace AndroidClient.Fragments
{
    public class Login : BaseFragment,
        IOnClickListener,
        IOnItemSelectedListener,
        ITextWatcher,
        IOnFocusChangeListener
    {
        public Spinner ServerTypeView { get; set; }
        public EditText ServerNameView { get; set; }
        public EditText UsernameView { get; set; }
        public EditText PasswordView { get; set; }
        public CheckBox RememberMeView { get; set; }
        public Button LoginButtonView { get; set; }
        private TokenProvider _persistenceProvider;
        private AppSettings _appSettings;
        private AuthService _service;
        public Client.Models.Login LoginModel;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Login, container, false);

            var actionBar = Activity.SupportActionBar;
            actionBar.Title = "Login";

            LoginButtonView = view.FindViewById<Button>(Resource.Id.loginButton);
            ServerTypeView = view.FindViewById<Spinner>(Resource.Id.serverTypes);
            ServerNameView = view.FindViewById<EditText>(Resource.Id.serverName);
            UsernameView = view.FindViewById<EditText>(Resource.Id.username);
            PasswordView = view.FindViewById<EditText>(Resource.Id.password);
            RememberMeView = view.FindViewById<CheckBox>(Resource.Id.rememberMe);

            UsernameView.AddTextChangedListener(this);
            PasswordView.AddTextChangedListener(this);

            UsernameView.OnFocusChangeListener = this;
            PasswordView.OnFocusChangeListener = this;

            LoginButtonView.SetOnClickListener(this);
            ServerTypeView.OnItemSelectedListener = this;

            using (var cts = new CancellationTokenSource())
            {
                _appSettings = ConfigurationManager.Instance.GetAsync(cts.Token).Result;
            }

            _service = new AuthService(Activity);
            _persistenceProvider = new TokenProvider(Activity, _appSettings);

            LoginModel = _persistenceProvider.GetCredentials();

            if(LoginModel != null)
            {
                ServerNameView.Text = LoginModel.ServerName;
                UsernameView.Text = LoginModel.Username;
                PasswordView.Text = LoginModel.Password;
                RememberMeView.Checked = LoginModel.RememberMe;
            }
            else
            {
                LoginModel = new Client.Models.Login();
            }

            var items = Resources.GetStringArray(Resource.Array.ServerTypes);
            
            var adapter = new ArrayAdapter<string>(Activity, Resource.Layout.ServerTypeSpinnerItem, items);
            ServerTypeView.Adapter = adapter;
            
            return view;
        }


        public async void OnClick(View v)
        {
            Activity.NavigationView.Visibility = ViewStates.Visible;
            Activity.ActivityMainLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedOpen);
            
            LoginModel.ServerName = ServerNameView.Text;
            LoginModel.Username = UsernameView.Text;
            LoginModel.Password = PasswordView.Text;
            LoginModel.RememberMe = RememberMeView.Checked;

            var result = await _service.Login(LoginModel);

            if(result.Error != null)
            {
                //var errorMessage = result.Error.Values.FirstOrDefault();
                //Toast.MakeText(Activity, errorMessage,ToastLength.Short);

                return;
            }

            if (RememberMeView.Checked)
            {
                _persistenceProvider.SetCredentials(LoginModel);
            }
            else
            {
                _persistenceProvider.ClearCredentials();
            }

            NavigationManager.GoToGoodsReceivedNotes();
            
        }

        private void Validate()
        {
            LoginButtonView.Enabled = !string.IsNullOrEmpty(UsernameView.Text) &&
                !string.IsNullOrEmpty(UsernameView.Text);
        }

        
        public void AfterTextChanged(IEditable s)
        {
            Validate();
        }

        public void OnFocusChange(View view, bool hasFocus)
        {
            if (!hasFocus)
            {
                var textView = (EditText)view;

                if (string.IsNullOrEmpty(textView.Text))
                {
                    textView.SetError("Field cannot be empty",null);
                }
                else
                {
                    textView.SetError((string)null,null);
                }
            }

            Validate();
        }

        public void OnItemSelected(AdapterView parent, View view, int position, long id) { }
        public void OnNothingSelected(AdapterView parent) { }
    }
}