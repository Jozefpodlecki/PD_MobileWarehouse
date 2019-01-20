using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Models;
using Client.Services;
using Client.ViewHolders;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Android.Widget.CompoundButton;

namespace Client.Fragments.Add
{
    public class User : BaseAddFragment<Models.User>,
        AdapterView.IOnItemSelectedListener,
        IOnCheckedChangeListener
    {
        public EditText AddUserName { get; set; }
        public EditText AddUserEmail { get; set; }
        public EditText AddUserPassword { get; set; }
        public Spinner AddUserRolesList { get; set; }
        public ListView AddUserPermissionsList { get; set; }
        private CheckBoxPermissionsAdapter _permissionAdapter;
        private SpinnerDefaultValueAdapter<Models.Role> _roleSpinnerAdapter;

        public User() : base(Resource.Layout.AddUser)
        {
            Entity = new Models.User();
        }

        public static Dictionary<int, Action<Models.User, object>> ViewToObjectMap = new Dictionary<int, Action<Models.User, object>>()
        {
            { Resource.Id.AddUserUsername, (model, data) => { model.Username = (string)data; } },
            { Resource.Id.AddUserEmail, (model, data) => { model.Email = (string)data; } },
            { Resource.Id.AddUserPassword, (model, data) => { model.Password = (string)data; } },
            { Resource.Id.AddUserRolesList, (model, data) => { model.Role = (Models.Role)data; } }
        };

        public override void OnBindElements(View view)
        {
            AddUserName = view.FindViewById<EditText>(Resource.Id.AddUserUsername);
            AddUserEmail = view.FindViewById<EditText>(Resource.Id.AddUserEmail);
            AddUserPassword = view.FindViewById<EditText>(Resource.Id.AddUserPassword);
            AddUserRolesList = view.FindViewById<Spinner>(Resource.Id.AddUserRolesList);
            AddUserPermissionsList = view.FindViewById<ListView>(Resource.Id.AddUserPermissionsList);

            AddUserName.AfterTextChanged += AfterTextChanged;
            AddUserEmail.AfterTextChanged += AfterTextChanged;
            AddUserPassword.AfterTextChanged += AfterTextChanged;

            AddUserRolesList.OnItemSelectedListener = this;

            _roleSpinnerAdapter = new SpinnerDefaultValueAdapter<Models.Role>(Context);
            _roleSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            AddUserRolesList.Adapter = _roleSpinnerAdapter;

            Task.Run(Load);
        }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(AddUserName.Text)
                && !string.IsNullOrEmpty(AddUserEmail.Text)
                && !string.IsNullOrEmpty(AddUserPassword.Text)
                && !string.IsNullOrEmpty(AddUserRolesList.SelectedItem.ToString());
        }

        public async override Task OnAddButtonClick(CancellationToken token)
        {
            Entity.Role = (Models.Role)AddUserRolesList.SelectedItem;
            Entity.Claims = _permissionAdapter.SelectedItems;
      
            var result = await UserService.AddUser(Entity, token);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    AddButton.Enabled = true;
                    ShowToastMessage("An error occurred");
                });

                return;
            }

            RunOnUiThread(() =>
            {
                NavigationManager.GoToUsers();
            });
        }

        public override void OnOtherButtonClick(View view)
        {
            var holder = (CheckBoxRowItemViewHolder)view.Tag;
            holder.Permission.Checked = !holder.Permission.Checked;
            _permissionAdapter.GetItem(holder.Position).Checked = holder.Permission.Checked;
        }

        public void OnItemSelected(AdapterView parent, View view, int position, long id)
        {
            if (position == 0)
            {
                return;
            }

            var role = (Models.Role)parent.GetItemAtPosition(position);

            ViewToObjectMap[parent.Id](Entity, role);
        }

        public void OnNothingSelected(AdapterView parent)
        {
            ViewToObjectMap[parent.Id](Entity, null);
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            Validate();

            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();
            ValidateRequired(editText);
            ViewToObjectMap[editText.Id](Entity, text);
        }

        public async Task Load()
        {
            var result = await RoleService.GetRoles(Criteria);

            var claimsResult = await RoleService.GetClaims();

            if (result.Error.Any() || claimsResult.Error.Any())
            {
                ShowToastMessage(Resource.String.ErrorOccurred);

                return;
            }

            var roles = result.Data;

            RunOnUiThread(() =>
            {
                roles.Insert(0, new Models.Role { Id = -1, Name = "Wybierz rolę" });
                _roleSpinnerAdapter.AddAll(roles);                

                _permissionAdapter = new CheckBoxPermissionsAdapter(Context, claimsResult.Data);
                AddUserPermissionsList.Adapter = _permissionAdapter;
                _permissionAdapter.IOnCheckedChangeListener = this;
                _permissionAdapter.IOnClickListener = this;
            });
            
        }

        public void OnCheckedChanged(CompoundButton view, bool isChecked)
        {
            var holder = (CheckBoxRowItemViewHolder)view.Tag;
            _permissionAdapter.GetItem(holder.Position).Checked = isChecked;
        }

    }
}