using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common;
using Java.Lang;

namespace Client.Fragments
{
    public class Users : Fragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddUserFloatButton { get; set; }
        public RecyclerView UserList { get; set; }
        public AutoCompleteTextView SearchUser { get; set; }
        public FilterCriteria Criteria { get; set; }
        public new MainActivity Activity => (MainActivity)base.Activity;
        private UserAdapter _adapter;
        private UserService _service;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Users, container, false);

            AddUserFloatButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddUserFloatActionButton);
            UserList = view.FindViewById<RecyclerView>(Resource.Id.UserList);
            SearchUser = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchUser);

            AddUserFloatButton.SetOnClickListener(this);
            SearchUser.AddTextChangedListener(this);

            _service = new UserService(Activity);

            Criteria = new FilterCriteria
            {
                ItemsPerPage = 10,
                Page = 0
            };

            var users = new List<Common.DTO.User>();

            _adapter = new UserAdapter(Activity, users);
            UserList.SetAdapter(_adapter);

            GetUsers();

            return view;
        }

        public void GetUsers()
        {
            List<Common.DTO.User> users = null;

            var task = Task.Run(async () =>
            {
                users = await _service.GetUsers(Criteria);
            });

            task.Wait();

            _adapter.UpdateList(users);
        }

        public void OnClick(View view)
        {
            Activity.NavigationManager.GoToAddUser();
        }

        public void AfterTextChanged(IEditable s)
        {
            
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after) { }
        public void OnTextChanged(ICharSequence s, int start, int before, int count) { }
    }
}