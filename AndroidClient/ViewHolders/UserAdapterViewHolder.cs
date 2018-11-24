using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class UserAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView UserRowItemName { get; set; }
        public TextView UserRowItemRole { get; set; }
        public ImageButton UserRowItemDelete { get; set; }

        public UserAdapterViewHolder(View itemView) : base(itemView)
        {
            UserRowItemName = itemView.FindViewById<TextView>(Resource.Id.UserRowItemName);
            UserRowItemRole = itemView.FindViewById<TextView>(Resource.Id.UserRowItemRole);
            UserRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.UserRowItemDelete);
        }

    }
}