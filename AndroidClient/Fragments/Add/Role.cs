using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.OS;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common;
using static Android.Views.View;

namespace Client.Fragments.Add
{
    public class Role : BaseFragment,
        View.IOnClickListener,
        IOnFocusChangeListener
    {
        public EditText AddRoleName { get; set; }
        public ListView AddRolePermissionsList { get; set; }
        public Button AddRoleButton { get; set; }
        private AddUserPermissionsAdapter _addUserPermissionsAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddRole, container, false);

            //var actionBar = Activity.SupportActionBar;
            //actionBar.Title = "Add Role";

            AddRoleName = view.FindViewById<EditText>(Resource.Id.AddRoleName);
            AddRolePermissionsList = view.FindViewById<ListView>(Resource.Id.AddRolePermissionsList);
            AddRoleButton = view.FindViewById<Button>(Resource.Id.AddRoleButton);

            AddRoleButton.SetOnClickListener(this);

            AddRoleName.OnFocusChangeListener = this;

            HttpResult<List<Models.Claim>> result = null;
            
            var task = Task.Run(async () =>
            {
                result = await RoleService.GetClaims();
            });

            task.Wait();

            _addUserPermissionsAdapter = new AddUserPermissionsAdapter(Context, result.Data);

            AddRolePermissionsList.Adapter = _addUserPermissionsAdapter;

            AddRoleButton.Enabled = false;

            return view;
        }

        public async void OnFocusChange(View view, bool hasFocus)
        {
            if(view == AddRoleName)
            {
                if (!hasFocus)
                {
                    if (string.IsNullOrEmpty(AddRoleName.Text))
                    {
                        AddRoleName.SetError("Field is required", null);
                        AddRoleButton.Enabled = false;

                        return;
                    }

                    var result = await RoleService.RoleExists(AddRoleName.Text);

                    if(result.Error != null)
                    {
                        ShowToastMessage("An error occurred");
                        AddRoleButton.Enabled = false;

                        return;
                    }

                    if (result.Data)
                    {
                        AddRoleName.SetError("Role with that name exists", null);
                        AddRoleButton.Enabled = false;
                    }

                    AddRoleName.SetError((string)null, null);
                    AddRoleButton.Enabled = true;
                }
            }
        }

        public void Validate()
        {

        }
        
        public async void OnClick(View view)
        {
            Validate();

            var claims = _addUserPermissionsAdapter
                .Items
                .Where(it => it.Checked)
                .ToList();
            
            var role = new Models.Role
            {
                Name = AddRoleName.Text,
                Claims = claims
            };

            var result = await RoleService.AddRole(role);

            if(result.Error != null)
            {
                ShowToastMessage("An error occurred");

                return;
            }

            NavigationManager.GoToRoles();
        }

    }
}