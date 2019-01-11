using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Client.ViewHolders;
using Common;
using static Android.Views.View;
using static Android.Widget.CompoundButton;

namespace Client.Fragments.Add
{
    public class Role : BaseAddFragment<Models.Role>,
        IOnCheckedChangeListener
    {
        public EditText AddRoleName { get; set; }
        public ListView AddRolePermissionsList { get; set; }
        private CheckBoxPermissionsAdapter _permissionAdapter;

        public Role() : base(Resource.Layout.AddRole)
        {
            Entity = new Models.Role();
        }

        public static Dictionary<int, Action<Models.Role, object>> ViewToObjectMap = new Dictionary<int, Action<Models.Role, object>>()
        {
            { Resource.Id.AddRoleName, (model, text) => { model.Name = (string)text; } },
        };

        public override void OnBindElements(View view)
        {
            AddRoleName = view.FindViewById<EditText>(Resource.Id.AddRoleName);
            AddRolePermissionsList = view.FindViewById<ListView>(Resource.Id.AddRolePermissionsList);

            AddRoleName.AfterTextChanged += AfterTextChanged;

            Task.Run(Load);
        }

        public override bool Validate()
        {
            if (ValidateRequired(AddRoleName))
            {
                return false;
            }

            if (!_permissionAdapter.SelectedItems.Any())
            {
                ShowToastMessage(Resource.String.ClaimsRequired);
                
                return false;
            }

            return true;
        }

        public override void OnOtherButtonClick(View view)
        {
            var holder = (CheckBoxRowItemViewHolder)view.Tag;
            holder.Permission.Checked = !holder.Permission.Checked;
            _permissionAdapter.GetItem(holder.Position).Checked = holder.Permission.Checked;
        }

        public override async Task OnAddButtonClick(CancellationToken token)
        {
            Entity.Claims = _permissionAdapter.SelectedItems;

            var result = await RoleService.AddRole(Entity);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    ShowToastMessage(Resource.String.ErrorOccurred);
                    AddButton.Enabled = true;
                });

                return;
            }

            RunOnUiThread(() =>
            {
                NavigationManager.GoToRoles();
            });
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            var validated = ValidateRequired(editText);
            AddButton.Enabled = validated;

            ViewToObjectMap[editText.Id](Entity, text);
        }

        public async Task Load()
        {
            var result = await RoleService.GetClaims();

            if (result.Error.Any())
            {
                ShowToastMessage("An error occurred");

                return;
            }

            RunOnUiThread(() =>
            {
                _permissionAdapter = new CheckBoxPermissionsAdapter(Context, result.Data);

                AddRolePermissionsList.Adapter = _permissionAdapter;
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