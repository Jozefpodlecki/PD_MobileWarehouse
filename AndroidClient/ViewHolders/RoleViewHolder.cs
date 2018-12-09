using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class RoleViewHolder : RecyclerView.ViewHolder
    {
        public TextView RoleRowItemName { get; set; }
        public ImageButton RoleRowItemDelete { get; set; }

        public RoleViewHolder(View itemView) : base(itemView)
        {
            RoleRowItemName = itemView.FindViewById<TextView>(Resource.Id.RoleRowItemName);
            RoleRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.RoleRowItemDelete);

            RoleRowItemDelete.Tag = this;
        }
    }
}