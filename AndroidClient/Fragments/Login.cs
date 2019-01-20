using Android.OS;
using Android.Text;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Client.Listeners;
using System.Linq;
using System.Threading.Tasks;
using static Android.Views.View;

namespace Client.Fragments
{
    public class Login : BaseFragment,
        IOnClickListener,
        ITextWatcher,
        IOnFocusChangeListener
    {
        public EditText ServerName { get; set; }
        public EditText Username { get; set; }
        public EditText Password { get; set; }
        public CheckBox RememberMe { get; set; }
        public Button LoginButton { get; set; }
        public Button DemoButton { get; set; }
        public ProgressBar LoginProgressBar { get; set; }
        public LinearLayout LoginLayout { get; set; }
        private Client.Models.Login LoginModel;
        public IOnServerProvidedListener OnServerProvidedListener { get; set; }
        public IOnLoginListener OnLoginListener { get; set; }

        public Login() : base(Resource.Layout.Login)
        {
        }

        public override void OnBindElements(View view)
        {
            LoginButton = view.FindViewById<Button>(Resource.Id.loginButton);
            DemoButton = view.FindViewById<Button>(Resource.Id.DemoButton);
            ServerName = view.FindViewById<EditText>(Resource.Id.serverName);
            Username = view.FindViewById<EditText>(Resource.Id.username);
            Password = view.FindViewById<EditText>(Resource.Id.password);
            RememberMe = view.FindViewById<CheckBox>(Resource.Id.rememberMe);
            LoginProgressBar = view.FindViewById<ProgressBar>(Resource.Id.LoginProgressBar);
            LoginLayout = view.FindViewById<LinearLayout>(Resource.Id.LoginLayout);

            LoginProgressBar.Visibility = ViewStates.Invisible;
            LoginButton.Enabled = false;

            Username.AddTextChangedListener(this);
            Password.AddTextChangedListener(this);
            Username.OnFocusChangeListener = this;
            Password.OnFocusChangeListener = this;
            LoginButton.SetOnClickListener(this);
            DemoButton.SetOnClickListener(this);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            OnServerProvidedListener = Activity;
            OnLoginListener = Activity;

            LoginModel = PersistenceProvider.GetCredentials();

            if (LoginModel != null)
            {
                ServerName.Text = LoginModel.ServerName;
                Username.Text = LoginModel.Username;
                Password.Text = LoginModel.Password;
                RememberMe.Checked = LoginModel.RememberMe;
            }
            else
            {
                LoginModel = new Models.Login();
            }

            LoginModel.ServerName = "http://10.0.2.2/MobileWarehouseServer";
            LoginModel.Username = "admin1";
            LoginModel.Password = "123";
            ServerName.Text = LoginModel.ServerName;
            Username.Text = LoginModel.Username;
            Password.Text = LoginModel.Password;
        }

        public void SetEnabled(bool state)
        {
            LoginButton.Enabled = state;
            ServerName.Enabled = state;
            Username.Enabled = state;
            Password.Enabled = state;
            RememberMe.Enabled = state;
        }

        public void OnClick(View view)
        {
            if(view.Id == DemoButton.Id)
            {
                ServerName.Text = Constants.Localhost;
                Username.Text = "admin1";
                Password.Text = "123";
                RememberMe.Checked = false;
            }

            LoginModel.ServerName = ServerName.Text;
            LoginModel.Username = Username.Text;
            LoginModel.Password = Password.Text;
            LoginModel.RememberMe = RememberMe.Checked;

            var token = CancelAndSetTokenForView(view);

            SetEnabled(false);

            var animationFadeOut = AnimationUtils.LoadAnimation(Context, Android.Resource.Animation.FadeOut);
            animationFadeOut.Duration = 500;
            var animationFadeIn = AnimationUtils.LoadAnimation(Context, Android.Resource.Animation.FadeIn);
            animationFadeIn.Duration = 500;

            animationFadeIn.SetAnimationListener(new VisibilityAnimationListener(LoginProgressBar, ViewStates.Visible));
            animationFadeOut.SetAnimationListener(new VisibilityAnimationListener(LoginLayout, ViewStates.Invisible));

            LoginLayout.StartAnimation(animationFadeOut);
            LoginProgressBar.StartAnimation(animationFadeIn);

            OnServerProvidedListener.OnServerProvided(LoginModel.ServerName);

            Task.Run(async () =>
            {
                var result = await AuthService.Login(LoginModel, token);

                if (result.Error.Any())
                {
                    var errorMessage = result.Error.Select(kv => kv.Value.FirstOrDefault()).FirstOrDefault()
                        ?? "An error occurred";

                    RunOnUiThread(() =>
                    {
                        ShowToastMessage(errorMessage);

                        animationFadeOut.Cancel();
                        animationFadeIn.Cancel();

                        animationFadeIn.SetAnimationListener(new VisibilityAnimationListener(LoginLayout, ViewStates.Visible));
                        animationFadeOut.SetAnimationListener(new VisibilityAnimationListener(LoginProgressBar, ViewStates.Invisible));

                        LoginProgressBar.StartAnimation(animationFadeOut);
                        LoginLayout.StartAnimation(animationFadeIn);

                        SetEnabled(true);
                    });

                    return;
                }

                RunOnUiThread(() =>
                {
                    animationFadeOut.Cancel();
                    animationFadeIn.Cancel();

                    animationFadeIn.SetAnimationListener(new VisibilityAnimationListener(LoginLayout, ViewStates.Visible));
                    animationFadeOut.SetAnimationListener(new VisibilityAnimationListener(LoginProgressBar, ViewStates.Invisible));

                    LoginProgressBar.StartAnimation(animationFadeOut);
                    LoginLayout.StartAnimation(animationFadeIn);

                    SetEnabled(true);

                    if (LoginModel.RememberMe)
                    {
                        PersistenceProvider.SetCredentials(LoginModel);
                    }
                    else
                    {
                        PersistenceProvider.ClearCredentials();
                    }

                    OnLoginListener.OnLogin(LoginModel, result.Data);
                });

            }, token);
        }

        private void Validate()
        {
            LoginButton.Enabled =
                !string.IsNullOrEmpty(ServerName.Text) &&
                !string.IsNullOrEmpty(Username.Text) &&
                !string.IsNullOrEmpty(Password.Text);
        }

        
        public void AfterTextChanged(IEditable s)
        {
            Validate();
        }

        public void OnFocusChange(View view, bool hasFocus)
        {
            if (hasFocus)
            {
                return;
            }

            ValidateRequired((EditText)view);
            Validate();
        }
    }
}