
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Client.Helpers;
using Client.Managers;
using Client.Providers;
using Client.Services;
using Common;
using Java.Lang;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments
{
    public abstract class BaseFragment : Fragment
    {
        public new MainActivity Activity => (MainActivity)base.Activity;
        public NavigationManager NavigationManager => Activity.NavigationManager;
        public AttributeService AttributeService => Activity.AttributeService;
        public AuthService AuthService => Activity.AuthService;
        public CityService CityService => Activity.CityService;
        public CounterpartyService CounterpartyService => Activity.CounterpartyService;
        public InvoiceService InvoiceService => Activity.InvoiceService;
        public LocationService LocationService => Activity.HLocationService;
        public NoteService NoteService => Activity.NoteService;
        public ProductService ProductService => Activity.ProductService;
        public RoleService RoleService => Activity.RoleService;
        public UserService UserService => Activity.HUserService;
        public PersistenceProvider PersistenceProvider => Activity.PersistenceProvider;
        public RoleManager RoleManager => Activity.RoleManager;
        public CameraProvider CameraProvider;
        public virtual FilterCriteria Criteria { get; set; }
        public LinearLayoutManager LayoutManager { get; set; }
        public Android.Support.V7.App.ActionBar ActionBar => Activity.SupportActionBar;
        public View LayoutView { get; set; }
        public System.Globalization.Calendar Calendar => Activity.Calendar;
        public void RunOnUiThread(Action action) => Activity.RunOnUiThread(action);
        private int _layoutId;

        public BaseFragment(int layoutId)
        {
            _layoutId = layoutId;

            Criteria = new FilterCriteria
            {
                ItemsPerPage = 10,
                Page = 0
            };

            CameraProvider = new CameraProvider(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(_layoutId, container, false);
            OnBindElements(view);

            return view;
        }

        public abstract void OnBindElements(View view);

        public bool CheckForAuthorizationErrors(Dictionary<string, string[]> Error)
        {
            var constainsKey = Error.TryGetValue(Constants.Server, out string[] values);

            if (constainsKey && values.Contains(nameof(HttpStatusCode.Unauthorized)))
            {
                RunOnUiThread(() =>
                {
                    NavigationManager.GoToLogin();
                });

                return false;
            }

            if (constainsKey && values.Contains(nameof(HttpStatusCode.Forbidden)))
            {

                RunOnUiThread(() =>
                {
                    ShowToastMessage(Resource.String.NotSufficientPermissions);
                    GoToFirstAvailableLocation();
                });

                return false;
            }

            return true;
        }

        public void GoToFirstAvailableLocation()
        {
            var menuItemClaimMap = RoleManager
                .Permissions
                .FirstOrDefault(kv => kv.Value.Contains("Read"));

            Activity.NaviagtionMenuMap[menuItemClaimMap.Key]();
        }
        

        public void SetImage(string base64Image, ImageView imageView)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                return;
            }

            var byteArray = Convert.FromBase64String(base64Image);
            var bitmap = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);
            imageView.SetImageBitmap(bitmap);
        }

        public string GetString(string identifierName)
        {
            var packageName = Activity.PackageName;
            var resourceId = Resources.GetIdentifier(identifierName, "string", packageName);

            if(resourceId == 0)
            {
                return identifierName;
            }

            return Resources.GetString(resourceId);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTitle();
        }

        public void SetTitle()
        {
            var typ = GetType();

            ActionBar.Title = GetString(typ.FullName);
        }

        public bool CheckAndRequestPermission(string permissionName)
        {
            if (Activity.CheckSelfPermission(permissionName) == Permission.Denied)
            {
                RequestPermissions(new[] { permissionName }, 0);

                return false;
            }

            return true;
        }

        public bool HasPermission(string permissionName, IEnumerable<string> permissionNames, IEnumerable<Permission> permissionGrants)
        {
            return permissionNames
                .Zip(permissionGrants, (perm, res) => new KeyValuePair<string, Permission>(perm, res))
                .Any(kv => kv.Key == permissionName
                && kv.Value == Permission.Granted);
        }

        public void SetError(EditText item, int resourceStringId)
        {
            var textInputLayout = item.Parent.Parent as TextInputLayout;

            var error = Resources.GetString(resourceStringId);

            if (textInputLayout == null)
            {
                item.Error = error;
                return;
            }

            textInputLayout.Error = error;
        }

        public void ClearError(EditText item)
        {
            var textInputLayout = item.Parent.Parent as TextInputLayout;

            if (textInputLayout == null)
            {
                item.Error = null;
                return;
            }

            textInputLayout.Error = null;
        }

        public bool ValidateRequired(EditText item)
        {
            var textInputLayout = item.Parent.Parent as TextInputLayout;
            var isEmpty = string.IsNullOrEmpty(item.Text);
            string error = null;

            if (isEmpty)
            {
                error = Resources.GetString(Resource.String.FieldRequired);
            }

            if (textInputLayout == null)
            {
                RunOnUiThread(() =>
                {
                    item.Error = error;
                });
                
                return !isEmpty;
            }

            RunOnUiThread(() =>
            {
                textInputLayout.Error = error;
            });

            return !isEmpty;
        }

        List<string> GetGrantedPermissions(string appPackage)
        {
            var granted = new List<string>();
            try
            {
                var packageInfo = Activity.PackageManager.GetPackageInfo(appPackage, PackageInfoFlags.Permissions);
                for (int i = 0; i < packageInfo.RequestedPermissions.Count; i++)
                {
                    if ((packageInfo.RequestedPermissionsFlags[i] & (int)RequestedPermission.Granted ) != 0)
                    {
                        granted.Add(packageInfo.RequestedPermissions[i]);
                    }
                }
            }
            catch (Java.Lang.Exception e)
            {
            }
            return granted;
        }

        protected void ShowToastMessage(string message, ToastLength toastLength = ToastLength.Short)
        {
            Toast.MakeText(Context, message, toastLength).Show();
        }

        protected void ShowToastMessage(int resourceId, ToastLength toastLength = ToastLength.Short)
        {
            Toast.MakeText(Context, resourceId, toastLength).Show();
        }

        public void CancelToken(View view)
        {
            var wrapper = view.Tag as JavaObjectWrapper<CancellationTokenSource>;

            if (wrapper != null)
            {
                if (!wrapper.Data.IsCancellationRequested)
                {
                    wrapper.Data.Cancel();
                }
            }
        }

        public CancellationToken CancelAndSetTokenForView(View view)
        {
            var wrapper = view.Tag as JavaObjectWrapper<CancellationTokenSource>;

            if (wrapper != null)
            {
                if (!wrapper.Data.IsCancellationRequested)
                {
                    wrapper.Data.Cancel();
                }
            }

            wrapper = new JavaObjectWrapper<CancellationTokenSource>(new CancellationTokenSource());
            view.Tag = wrapper;

            return wrapper.Data.Token;
        }

        public virtual void CancelAll()
        {

        }

        public DatePickerDialog CreateDatePickerDialog(DatePickerDialog.IOnDateSetListener listener)
        {
            var locale = Locale.ForLanguageTag("pl_PL");
            var calendar = Android.Icu.Util.Calendar.GetInstance(locale);
            var year = calendar.Get(Android.Icu.Util.CalendarField.Year);
            var month = calendar.Get(Android.Icu.Util.CalendarField.Month);
            var day = calendar.Get(Android.Icu.Util.CalendarField.DayOfMonth);

            var dialog = new DatePickerDialog(
                Activity,
                Android.Resource.Style.ThemeHoloDialog,
                listener,
                year,
                month,
                day);

            return dialog;
        }

        public virtual void BeforeTextChanged(ICharSequence s, int start, int count, int after) { }
        public virtual void OnTextChanged(ICharSequence s, int start, int before, int count) { }
    }
}