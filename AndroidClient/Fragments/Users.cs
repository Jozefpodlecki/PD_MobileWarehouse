using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common;

namespace Client.Fragments
{
    public class Users : BaseListFragment
    {
        private UserAdapter _adapter;

        public Users() : base(
            PolicyTypes.Users.Add,
            Resource.String.UsersEmpty,
            Resource.String.TypeInUser
            )
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var token = CancelAndSetTokenForView(ItemList);

            SetLoadingContent();

            _adapter = new UserAdapter(Context, RoleManager);
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
            var result = await UserService.GetUsers(Criteria, token);

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
            var viewHolder = view.Tag as ViewHolders.UserAdapterViewHolder;

            if (view.Id == Resource.Id.UserRowItemDelete)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                _adapter.RemoveItem(item);

                Task.Run(async () =>
                {
                    await UserService.DeleteUser(item.Id);
                });
            }
            if (view.Id == Resource.Id.UserRowItemInfo)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToUserDetails(item);
            }
            if (view.Id == Resource.Id.UserRowItemEdit)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToEditUser(item);
            }
            if (view.Id == AddItemFloatActionButton.Id)
            {
                NavigationManager.GoToAddUser();
            }
        }
    }
}