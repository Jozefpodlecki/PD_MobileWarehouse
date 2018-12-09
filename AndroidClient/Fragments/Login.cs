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
using Android.Views.Animations;
using System;
using static Android.Views.Animations.Animation;
using System.Threading.Tasks;

namespace AndroidClient.Fragments
{
    public class Login : BaseFragment,
        IOnClickListener,
        IOnItemSelectedListener,
        ITextWatcher,
        IOnFocusChangeListener
    {
        public EditText ServerNameView { get; set; }
        public EditText UsernameView { get; set; }
        public EditText PasswordView { get; set; }
        public CheckBox RememberMeView { get; set; }
        public Button LoginButtonView { get; set; }
        public ProgressBar LoginProgressBar { get; set; }
        public RelativeLayout LoginLayout { get; set; }
        public Client.Models.Login LoginModel;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Login, container, false);
            
            LoginButtonView = view.FindViewById<Button>(Resource.Id.loginButton);
            ServerNameView = view.FindViewById<EditText>(Resource.Id.serverName);
            UsernameView = view.FindViewById<EditText>(Resource.Id.username);
            PasswordView = view.FindViewById<EditText>(Resource.Id.password);
            RememberMeView = view.FindViewById<CheckBox>(Resource.Id.rememberMe);
            LoginProgressBar = view.FindViewById<ProgressBar>(Resource.Id.LoginProgressBar);
            LoginLayout = view.FindViewById<RelativeLayout>(Resource.Id.LoginLayout);
            
            LoginProgressBar.Visibility = ViewStates.Invisible;
            LoginButtonView.Enabled = false;

            UsernameView.AddTextChangedListener(this);
            PasswordView.AddTextChangedListener(this);
            UsernameView.OnFocusChangeListener = this;
            PasswordView.OnFocusChangeListener = this;
            LoginButtonView.SetOnClickListener(this);

            LoginModel = TokenProvider.GetCredentials();

            if (LoginModel != null)
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

            return view;
        }

        public void SetEnabled(bool state)
        {
            LoginButtonView.Enabled = state;
            ServerNameView.Enabled = state;
            UsernameView.Enabled = state;
            PasswordView.Enabled = state;
            RememberMeView.Enabled = state;
        }

        public class VisibilityAnimationListener : Java.Lang.Object, IAnimationListener
        {
            private readonly View _view;
            private readonly ViewStates _viewState;

            public VisibilityAnimationListener(View view, ViewStates viewState)
            {
                _view = view;
                _viewState = viewState;
            }

            public void OnAnimationEnd(Animation animation)
            {
                _view.Visibility = _viewState;
            }

            public void OnAnimationRepeat(Animation animation)
            {
                
            }

            public void OnAnimationStart(Animation animation)
            {
                
            }
        }

        public void OnClick(View v)
        {
            var token = CancelAndSetTokenForView(v);

            SetEnabled(false);
            
            var animationFadeOut = AnimationUtils.LoadAnimation(Context, Android.Resource.Animation.FadeOut);
            animationFadeOut.Duration = 500;
            var animationFadeIn = AnimationUtils.LoadAnimation(Context, Android.Resource.Animation.FadeIn);
            animationFadeIn.Duration = 500;

            animationFadeIn.SetAnimationListener(new VisibilityAnimationListener(LoginProgressBar, ViewStates.Visible));
            animationFadeOut.SetAnimationListener(new VisibilityAnimationListener(LoginLayout, ViewStates.Invisible));

            LoginLayout.StartAnimation(animationFadeOut);
            LoginProgressBar.StartAnimation(animationFadeIn);

            LoginModel.ServerName = ServerNameView.Text;
            LoginModel.Username = UsernameView.Text;
            LoginModel.Password = PasswordView.Text;
            LoginModel.RememberMe = RememberMeView.Checked;

            Task.Run(async () =>
            {
                var result = await AuthService.Login(LoginModel, token);

                if (result.Error != null)
                {
                    var errorMessage = "An error occurred";

                    if (result.Error.Any())
                    {
                        foreach (var error in result.Error)
                        {
                            foreach (var errorValues in error.Value)
                            {
                                Constants.ErrorsMap.TryGetValue(errorValues, out int stringResourceId);

                                if (stringResourceId != 0)
                                {
                                    errorMessage = Resources.GetString(stringResourceId);
                                }

                                Activity.RunOnUiThread(() =>
                                {
                                    ShowToastMessage(errorMessage);
                                });
                            }
                        }
                    }
                    else
                    {
                        Activity.RunOnUiThread(() =>
                        {
                            ShowToastMessage(errorMessage);
                        });
                    }

                    Activity.RunOnUiThread(() =>
                    {
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

                if (LoginModel.RememberMe)
                {
                    TokenProvider.SetCredentials(LoginModel);

                }
                else
                {
                    TokenProvider.ClearCredentials();
                }

                TokenProvider.SaveToken(Activity, result.Data);

                Activity.RunOnUiThread(() =>
                {
                    Activity.UnlockMenu();
                    Activity.RestrictMenus();

                    NavigationManager.GoToGoodsReceivedNotes();
                });

            }, token);
        }

        private void Validate()
        {
            LoginButtonView.Enabled = !string.IsNullOrEmpty(UsernameView.Text) &&
                !string.IsNullOrEmpty(PasswordView.Text);
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