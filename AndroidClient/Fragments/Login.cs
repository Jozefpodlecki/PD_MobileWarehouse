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

namespace AndroidClient.Fragments
{
    public class Login : Fragment,
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
        public new MainActivity Activity => (MainActivity)base.Activity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Login, container, false);

            Activity.ActionBar.Title = "Login";

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

            var items = Resources.GetStringArray(Resource.Array.ServerTypes);
            
            var adapter = new ArrayAdapter<string>(Activity, Resource.Layout.ServerTypeSpinnerItem, items);
            ServerTypeView.Adapter = adapter;
            
            return view;
        }


        public async void OnClick(View v)
        {
            Activity.NavigationView.Visibility = ViewStates.Visible;
            Activity.ActivityMainLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
            Activity.NavigationManager.GoToUsers();
            return;

            var service = new AuthService(Activity);

            var loginModel = new Client.Models.Login
            {
                Username = UsernameView.Text,
                Password = PasswordView.Text,
                RememberMe = RememberMeView.Checked
            };

            var result = await service.Login(loginModel);

            if(result.Error != null)
            {
                //var errorMessage = result.Error.Values.FirstOrDefault();
                //Toast.MakeText(Activity, errorMessage,ToastLength.Short);
            }
            else
            {
                Activity.NavigationManager.GoToGoodsReceivedNotes();
            }
            
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

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after) { }
        public void OnItemSelected(AdapterView parent, View view, int position, long id) { }
        public void OnNothingSelected(AdapterView parent) { }
        public void OnTextChanged(ICharSequence s, int start, int before, int count) { }
    }
}