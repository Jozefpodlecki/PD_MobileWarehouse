using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class AttributesRowItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView AttributesRowItemName { get; set; }
        public ImageButton AttributesRowItemEdit { get; set; }
        public ImageButton AttributesRowItemDelete { get; set; }

        public AttributesRowItemViewHolder(View itemView) : base(itemView)
        {
            AttributesRowItemName = itemView.FindViewById<TextView>(Resource.Id.AttributesRowItemName);
            AttributesRowItemEdit = itemView.FindViewById<ImageButton>(Resource.Id.AttributesRowItemEdit);
            AttributesRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.AttributesRowItemDelete);

            AttributesRowItemEdit.Tag = this;
            AttributesRowItemDelete.Tag = this;
        }
    }
}