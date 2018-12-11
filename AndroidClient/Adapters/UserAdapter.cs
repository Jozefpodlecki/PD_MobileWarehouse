using Android.Content;
using Android.Views;
using Client.ViewHolders;
using System.Collections.Generic;

namespace Client.Adapters
{
    public class UserAdapter : BaseRecyclerViewAdapter<Models.User,UserAdapterViewHolder>
    {
        public UserAdapter(Context context) : base(context, Resource.Layout.UserRowItem)
        {
        }

        public override void BindItemToViewHolder(Models.User item, UserAdapterViewHolder viewHolder)
        {
            viewHolder.UserRowItemName.Text = item.Username;
            viewHolder.UserRowItemRole.Text = item.Role;
        }

        public override UserAdapterViewHolder CreateViewHolder(View view) => new UserAdapterViewHolder(view);

        public override void UpdateList(List<Models.User> items)
        {
            base.UpdateList(items);
        }
    }
}