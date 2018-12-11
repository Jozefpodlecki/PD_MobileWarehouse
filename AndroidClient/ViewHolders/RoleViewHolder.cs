using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class RoleViewHolder : RecyclerView.ViewHolder
    {
        public TextView RoleRowItemName { get; set; }
        public ImageButton RoleRowItemDelete { get; set; }
        public ImageButton RoleRowItemEdit { get; set; }
        public ImageButton RoleRowItemInfo { get; set; }

        public RoleViewHolder(View itemView) : base(itemView)
        {
            RoleRowItemName = itemView.FindViewById<TextView>(Resource.Id.RoleRowItemName);
            RoleRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.RoleRowItemDelete);
            RoleRowItemEdit = itemView.FindViewById<ImageButton>(Resource.Id.RoleRowItemEdit);
            RoleRowItemInfo = itemView.FindViewById<ImageButton>(Resource.Id.RoleRowItemInfo);

            RoleRowItemDelete.Tag = this;
            RoleRowItemEdit.Tag = this;
            RoleRowItemInfo.Tag = this;
        }
    }
}