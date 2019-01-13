using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.ViewHolders;
using System.Collections.Generic;

namespace Client.Adapters
{
    public class UserAdapter : BaseRecyclerViewAdapter<Models.User,UserAdapterViewHolder>
    {
        public ViewStates EditVisibility;
        public ViewStates DeleteVisibility;
        public ViewStates ReadVisibility;

        public UserAdapter(Context context, IRoleManager roleManager) : base(context, roleManager, Resource.Layout.UserRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.UserRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            ReadVisibility = roleManager.Permissions.ContainsKey(Resource.Id.UserRowItemInfo) ? ViewStates.Visible : ViewStates.Invisible;
            EditVisibility = roleManager.Permissions.ContainsKey(Resource.Id.UserRowItemEdit) ? ViewStates.Visible : ViewStates.Invisible;
        }

        public override void BindItemToViewHolder(Models.User item, UserAdapterViewHolder viewHolder)
        {
            viewHolder.UserRowItemName.Text = item.Username;
            viewHolder.UserRowItemRole.Text = item.Role.ToString();
            viewHolder.UserRowItemDelete.SetOnClickListener(IOnClickListener);
            viewHolder.UserRowItemEdit.SetOnClickListener(IOnClickListener);
            viewHolder.UserRowItemInfo.SetOnClickListener(IOnClickListener);

            viewHolder.UserRowItemDelete.Visibility = DeleteVisibility;
            viewHolder.UserRowItemEdit.Visibility = EditVisibility;
            viewHolder.UserRowItemInfo.Visibility = ReadVisibility;
        }

        public override UserAdapterViewHolder CreateViewHolder(View view) => new UserAdapterViewHolder(view);

        public override void UpdateList(List<Models.User> items)
        {
            base.UpdateList(items);
        }
    }
}