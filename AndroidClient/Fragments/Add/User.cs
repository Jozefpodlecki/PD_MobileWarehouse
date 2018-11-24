
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Android.Views.View;

namespace Client.Fragments.Add
{
    public class User : BaseFragment,
        View.IOnClickListener,
        AdapterView.IOnItemClickListener
    {
        public EditText AddUserName { get; set; }
        public EditText AddUserEmail { get; set; }
        public EditText AddUserPassword { get; set; }
        public Spinner RolesList { get; set; }
        public ListView AddUserPermissionsList { get; set; }
        public Button AddUserButton { get; set; }
        public new MainActivity Activity => (MainActivity)base.Activity;
        private UserService _userService;
        private AddUserPermissionsAdapter _addUserPermissionsAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddUser, container, false);

            AddUserName = view.FindViewById<EditText>(Resource.Id.AddUserName);
            AddUserEmail = view.FindViewById<EditText>(Resource.Id.AddUserEmail);
            AddUserPassword = view.FindViewById<EditText>(Resource.Id.AddUserPassword);
            RolesList = view.FindViewById<Spinner>(Resource.Id.AddUserRolesList);
            AddUserPermissionsList = view.FindViewById<ListView>(Resource.Id.AddUserPermissionsList);
            AddUserButton = view.FindViewById<Button>(Resource.Id.AddUserButton);

            AddUserButton.SetOnClickListener(this);
            AddUserPermissionsList.OnItemClickListener = this;

            _userService = new UserService(Activity);
            var roleService = new RoleService(Activity);
            List<Claim> claims = null;
            HttpResult<List<Common.DTO.Role>> result = null;
            HttpResult<List<Claim>> claimsResult = null;

            var task = Task.Run(async () =>
            {
                result = await roleService
                        .GetRoles(Criteria);

                claimsResult = await roleService.GetClaims();
            });

            task.Wait();

            if(result.Error != null)
            {
                ShowToastMessage("An error occurred");

                return view;
            }

            var roles = result.Data;

            var spinnerAdapter = new ArrayAdapter<string>(Context,
                Android.Resource.Layout.SimpleSpinnerItem,
                roles.Select(ro => ro.Name).ToList()
                );

            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            RolesList.Adapter = spinnerAdapter;

            _addUserPermissionsAdapter = new AddUserPermissionsAdapter(Context, claimsResult.Data);

            AddUserPermissionsList.Adapter = _addUserPermissionsAdapter;

            return view;
        }

        public async void OnClick(View view)
        {
            var claims = _addUserPermissionsAdapter.Items
                .Where(it => it.Checked)
                .ToList();

            var user = new Common.DTO.User
            {
                Username = AddUserName.Text,
                Password = AddUserPassword.Text,
                Email = AddUserEmail.Text,
                Role = RolesList.SelectedItem.ToString(),
                Claims = claims
            };

            await _userService.AddUser(user);

            NavigationManager.GoToUsers();
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            
        }
             
    }
}