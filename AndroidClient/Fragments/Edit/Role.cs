using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.ViewHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Android.Widget.CompoundButton;

namespace Client.Fragments.Edit
{
    public class Role : BaseEditFragment<Models.Role>,
        IOnCheckedChangeListener
    {
        private CheckBoxPermissionsAdapter _adapter;
        public EditText RoleEditName { get; set; }
        public ListView RoleEditClaims { get; set; }

        public static Dictionary<int, Action<Models.Role, object>> ViewToObjectMap = new Dictionary<int, Action<Models.Role, object>>()
        {
            { Resource.Id.RoleEditName, (model, text) => { model.Name = (string)text; } },
        };

        public Role() : base(Resource.Layout.RoleEdit)
        {
        }

        public override void OnBindElements(View view)
        {
            RoleEditName = view.FindViewById<EditText>(Resource.Id.RoleEditName);
            RoleEditClaims = view.FindViewById<ListView>(Resource.Id.RoleEditClaims);

            RoleEditName.AfterTextChanged += AfterTextChanged;

            Entity = (Models.Role)Arguments.GetParcelable(Constants.Entity);
            RoleEditName.Text = Entity.Name;

            Task.Run(Load);
        }

        public override void OnOtherButtonClick(View view)
        {
            var holder = (CheckBoxRowItemViewHolder)view.Tag;
            holder.Permission.Checked = !holder.Permission.Checked;
            _adapter.GetItem(holder.Position).Checked = holder.Permission.Checked;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            var validated = ValidateRequired(editText);
            SaveButton.Enabled = validated;

            ViewToObjectMap[editText.Id](Entity, text);
        }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(RoleEditName.Text);
        }

        public async Task Load()
        {
            var result = await RoleService.GetClaims();

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    ShowToastMessage("An error occurred");
                });

                return;
            }

            var items = result.Data.Join(
                    Entity.Claims,
                    cl => cl.Value,
                    cl => cl.Value,
                    (en, cls) => en);

            foreach (var item in items)
            {
                item.Checked = true;
            }

            RunOnUiThread(() =>
            {
                _adapter = new CheckBoxPermissionsAdapter(Context, result.Data);
                RoleEditClaims.Adapter = _adapter;
                _adapter.IOnCheckedChangeListener = this;
                _adapter.IOnClickListener = this;
            });

        }

        public void OnCheckedChanged(CompoundButton view, bool isChecked)
        {
            var holder = (CheckBoxRowItemViewHolder)view.Tag;
            _adapter.GetItem(holder.Position).Checked = isChecked;
        }

        public override async Task OnSaveButtonClick(CancellationToken token)
        {
            Entity.Claims = _adapter.SelectedItems;

            if (!Entity.Claims.Any())
            {
                ShowToastMessage(Resource.String.ClaimsRequired);
                RoleEditClaims.RequestFocus();
                SaveButton.Enabled = true;

                return;
            }

            var result = await RoleService.UpdateRole(Entity, token);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    SaveButton.Enabled = true;
                    ShowToastMessage("An error occurred");
                });

                return;
            }

            RunOnUiThread(() =>
            {
                NavigationManager.GoToRoles();
            });
        }
    }
}