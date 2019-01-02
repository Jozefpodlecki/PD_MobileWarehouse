using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.ViewHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Android.Widget.CompoundButton;

namespace Client.Fragments.Edit
{
    public class Role : BaseFragment,
        View.IOnClickListener,
        IOnCheckedChangeListener
    {
        private CheckBoxPermissionsAdapter _adapter;
        public Button SaveRoleButton { get; set; }
        public EditText RoleEditName { get; set; }
        public ListView RoleEditClaims { get; set; }
        public Models.Role Entity { get; set; }

        public static Dictionary<int, Action<Models.Role, object>> ViewToObjectMap = new Dictionary<int, Action<Models.Role, object>>()
        {
            { Resource.Id.RoleEditName, (model, text) => { model.Name = (string)text; } },
        };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.RoleEdit, container, false);

            SaveRoleButton = view.FindViewById<Button>(Resource.Id.SaveRoleButton);
            RoleEditName = view.FindViewById<EditText>(Resource.Id.RoleEditName);
            RoleEditClaims = view.FindViewById<ListView>(Resource.Id.RoleEditClaims);

            RoleEditName.AfterTextChanged += AfterTextChanged;
            SaveRoleButton.SetOnClickListener(this);

            Entity = (Models.Role)Arguments.GetParcelable(Constants.Entity);
            RoleEditName.Text = Entity.Name;

            Task.Run(Load);

            return view;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            var validated = ValidateRequired(editText);
            SaveRoleButton.Enabled = validated;

            ViewToObjectMap[editText.Id](Entity, text);
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

        public void OnClick(View view)
        {
            if (view.Id == Resource.Id.CheckBoxRowItem)
            {
                var holder = (CheckBoxRowItemViewHolder)view.Tag;
                holder.Permission.Checked = !holder.Permission.Checked;
                _adapter.GetItem(holder.Position).Checked = holder.Permission.Checked;
            }
            if (view.Id == SaveRoleButton.Id)
            {
                var token = CancelAndSetTokenForView(view);

                SaveRoleButton.Enabled = false;

                Entity.Claims = _adapter.SelectedItems;

                if (!Entity.Claims.Any())
                {
                    ShowToastMessage(Resource.String.ClaimsRequired);
                    RoleEditClaims.RequestFocus();
                    SaveRoleButton.Enabled = true;

                    return;
                }

                Task.Run(async () =>
                {
                    var result = await RoleService.UpdateRole(Entity, token);

                    if (result.Error.Any())
                    {
                        RunOnUiThread(() =>
                        {
                            SaveRoleButton.Enabled = true;
                            ShowToastMessage("An error occurred");
                        });

                        return;
                    }

                    RunOnUiThread(() =>
                    {
                        NavigationManager.GoToRoles();
                    });
                });
            }
        }
    }
}