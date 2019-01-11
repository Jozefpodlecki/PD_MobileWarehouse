using Android.OS;
using Android.Support.Design.Widget;
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
    public class User : BaseEditFragment<Models.User>,
        IOnCheckedChangeListener,
        AdapterView.IOnItemSelectedListener
    {
        private CheckBoxPermissionsAdapter _adapter;
        public EditText UserEditUsername { get; set; }
        public EditText UserEditEmail { get; set; }
        public Spinner UserEditRolesList { get; set; }
        public EditText UserEditPassword { get; set; }
        public ListView UserEditClaims { get; set; }
        private SpinnerDefaultValueAdapter<Models.Role> _roleSpinnerAdapter;

        public static Dictionary<int, Action<Models.User, object>> ViewToObjectMap = new Dictionary<int, Action<Models.User, object>>()
        {
            { Resource.Id.UserEditUsername, (model, data) => { model.Username = (string)data; } },
            { Resource.Id.UserEditEmail, (model, data) => { model.Email = (string)data; } },
            { Resource.Id.UserEditPassword, (model, data) => { model.Password = (string)data; } },
            { Resource.Id.UserEditRolesList, (model, data) => { model.Role = (Models.Role)data; } }
        };

        public User() : base(Resource.Layout.UserEdit)
        {
        }

        public override void OnBindElements(View view)
        {
            UserEditUsername = view.FindViewById<EditText>(Resource.Id.UserEditUsername);
            UserEditEmail = view.FindViewById<EditText>(Resource.Id.UserEditEmail);
            UserEditRolesList = view.FindViewById<Spinner>(Resource.Id.UserEditRolesList);
            UserEditPassword = view.FindViewById<EditText>(Resource.Id.UserEditPassword);
            UserEditClaims = view.FindViewById<ListView>(Resource.Id.UserEditClaims);

            UserEditUsername.AfterTextChanged += AfterTextChanged;
            UserEditEmail.AfterTextChanged += AfterTextChanged;
            UserEditPassword.AfterTextChanged += AfterTextChanged;

            UserEditRolesList.OnItemSelectedListener = this;

            _roleSpinnerAdapter = new SpinnerDefaultValueAdapter<Models.Role>(Context);
            _roleSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            UserEditRolesList.Adapter = _roleSpinnerAdapter;

            UserEditUsername.Text = Entity.Username;
            UserEditEmail.Text = Entity.Email;
        }

        public void OnItemSelected(AdapterView parent, View view, int position, long id)
        {
            if(position == 0)
            {
                return;
            }

            ViewToObjectMap[parent.Id](Entity, parent.GetItemAtPosition(position));
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
            if(editText.Id != Resource.Id.UserEditPassword)
            {
                ValidateRequired(editText);
            }
            ViewToObjectMap[editText.Id](Entity, text);
        }

        public override bool Validate()
        {
            if (!string.IsNullOrEmpty(UserEditUsername.Text))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(UserEditEmail.Text))
            {
                return false;
            }

            return true;
        }

        public async Task Load()
        {
            var result = await RoleService.GetClaims();
            var rolesResult = await RoleService.GetRoles(Criteria);
            var rolesPrompt = Resources.GetString(Resource.String.RolePrompt);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    ShowToastMessage(Resource.String.ErrorOccurred);
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
                UserEditClaims.Adapter = _adapter;
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

            var result = await UserService.UpdateUser(Entity, token);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    SaveButton.Enabled = true;
                    ShowToastMessage(Resource.String.ErrorOccurred);
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
            _adapter.GetItem(holder.Position).Checked = holder.Permission.Checked;
        }
        
    }
}