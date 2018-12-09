
using Android.App;
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
using System.Threading;
using static Android.Support.V7.Widget.RecyclerView;

namespace Client.Fragments
{
    public abstract class BaseFragment : Fragment
    {
        public new MainActivity Activity => (MainActivity)base.Activity;
        public NavigationManager NavigationManager => Activity.NavigationManager;
        public AuthService AuthService => Activity.AuthService;
        public LocationService LocationService => Activity.HLocationService;
        public NoteService NoteService => Activity.NoteService;
        public ProductService ProductService => Activity.ProductService;
        public RoleService RoleService => Activity.RoleService;
        public UserService UserService => Activity.HUserService;
        public PersistenceProvider TokenProvider => Activity.PersistenceProvider;
        public FilterCriteria Criteria { get; set; }
        public LinearLayoutManager LayoutManager { get; set; }
        public View LayoutView { get; set; }
        public static System.Globalization.Calendar Calendar;
        //public static

        static BaseFragment()
        {
            Calendar = new System.Globalization.GregorianCalendar();
        }

        public BaseFragment()
        {
            Criteria = new FilterCriteria
            {
                ItemsPerPage = 10,
                Page = 0
            };
        }

        public void SetTitle()
        {
            //var actionBar = Activity.SupportActionBar;
            //actionBar.Title = "Login";
        }

        protected void ShowToastMessage(string message, ToastLength toastLength = ToastLength.Short)
        {
            Toast.MakeText(Context, message, toastLength).Show();
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