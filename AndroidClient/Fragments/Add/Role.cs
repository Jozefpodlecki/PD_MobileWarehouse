using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Role : BaseFragment,
        View.IOnClickListener,
        IOnCheckedChangeListener
    {
        public EditText AddRoleName { get; set; }
        public ListView AddRolePermissionsList { get; set; }
        public Button AddRoleButton { get; set; }
        private CheckBoxPermissionsAdapter _permissionAdapter;
        public Models.Role Entity { get; set; }

        public static Dictionary<int, Action<Models.Role, object>> ViewToObjectMap = new Dictionary<int, Action<Models.Role, object>>()
        {
            { Resource.Id.AddRoleName, (model, text) => { model.Name = (string)text; } },
        };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddRole, container, false);

            AddRoleName = view.FindViewById<EditText>(Resource.Id.AddRoleName);
            AddRolePermissionsList = view.FindViewById<ListView>(Resource.Id.AddRolePermissionsList);
            AddRoleButton = view.FindViewById<Button>(Resource.Id.AddRoleButton);

            AddRoleName.AfterTextChanged += AfterTextChanged;
            AddRoleButton.SetOnClickListener(this);

            Entity = new Models.Role();

            Task.Run(Load);

            AddRoleButton.Enabled = false;
            
            return view;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            var validated = ValidateRequired(editText);
            AddRoleButton.Enabled = validated;

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

        public void OnClick(View view)
        {
            if(view.Id == Resource.Id.CheckBoxRowItem)
            {
                var holder = (CheckBoxRowItemViewHolder)view.Tag;
                holder.Permission.Checked = !holder.Permission.Checked;
                _permissionAdapter.GetItem(holder.Position).Checked = holder.Permission.Checked;
            }
            if(view.Id == AddRoleButton.Id)
            {
                AddRoleButton.Enabled = false;

                Entity.Claims = _permissionAdapter.SelectedItems;

                if (!Entity.Claims.Any())
                {
                    ShowToastMessage(Resource.String.ClaimsRequired);
                    AddRolePermissionsList.RequestFocus();
                    AddRoleButton.Enabled = true;

                    return;
                }

                Task.Run(async () =>
                {
                    var result = await RoleService.AddRole(Entity);

                    if (result.Error.Any())
                    {
                        RunOnUiThread(() =>
                        {
                            ShowToastMessage("An error occurred");
                            AddRoleButton.Enabled = true;
                        });

                        return;
                    }

                    NavigationManager.GoToRoles();
                });
            }   
        }
    }
}