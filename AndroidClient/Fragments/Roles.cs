using Android.OS;
using Android.Text;
using Android.Views;
using Client.Adapters;
using Client.Services;
using Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments
{
    public class Roles : BaseListFragment
    {
        private RoleAdapter _adapter;

        public Roles() : base(
            PolicyTypes.Roles.Add,
            Resource.String.RolesEmpty,
            Resource.String.TypeInRole
            )
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var token = CancelAndSetTokenForView(ItemList);

            SetLoadingContent();

            _adapter = new RoleAdapter(Context, RoleManager);
            _adapter.IOnClickListener = this;

            ItemList.SetAdapter(_adapter);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);

            return view;
        }

        public override async Task GetItems(CancellationToken token)
        {
            var result = await RoleService.GetRoles(Criteria);

            if (!CheckForAuthorizationErrors(result.Error)) return;

            RunOnUiThread(() =>
            {
                if (result.Error.Any())
                {
                    ShowToastMessage(Resource.String.ErrorOccurred);

                    return;
                }

                _adapter.UpdateList(result.Data);

                if (result.Data.Any())
                {
                    SetContent();

                    return;
                }

                SetEmptyContent();
            });
        }

        public override void OnClick(View view)
        {
            var viewHolder = view.Tag as ViewHolders.RoleViewHolder;

            if (view.Id == Resource.Id.RoleRowItemDelete)
            {
                Task.Run(async () =>
                {
                    var item = _adapter.GetItem(viewHolder.AdapterPosition);
                    var result = await RoleService.DeleteRole(item.Id);

                    if (result.Error.Any())
                    {
                        if (result.Error.ContainsKey("UserRoles"))
                        {
                            RunOnUiThread(() =>
                            {
                                ShowToastMessage(Resource.String.UsersAssignedToRole);
                            });

                            return;
                        }

                        RunOnUiThread(() =>
                        {
                            ShowToastMessage(Resource.String.ErrorOccurred);
                        });

                        return;
                    }

                    RunOnUiThread(() =>
                    {
                        _adapter.RemoveItem(item);
                    });
                });
            }
            if (view.Id == Resource.Id.RoleRowItemEdit)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToEditRole(item);
            }
            if (view.Id == Resource.Id.RoleRowItemInfo)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToRoleDetails(item);
            }
            if (view.Id == AddItemFloatActionButton.Id)
            {
                NavigationManager.GoToAddRole();
            }
        }
    }
}