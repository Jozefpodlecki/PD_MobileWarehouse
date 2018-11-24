using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class LocationRowItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView LocationRowItemName { get; set; }
        public ImageButton LocationRowItemDelete { get; set; }

        public LocationRowItemViewHolder(View itemView) : base(itemView)
        {
            LocationRowItemName = itemView.FindViewById<TextView>(Resource.Id.LocationRowItemName);
            LocationRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.LocationRowItemDelete);

            LocationRowItemDelete.Tag = this;
        }
    }
}