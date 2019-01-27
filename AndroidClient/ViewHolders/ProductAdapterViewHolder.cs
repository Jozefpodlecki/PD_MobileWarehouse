using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class ProductAdapterViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ProductRowItemImage { get; set; }
        public TextView ProductRowItemName { get; set; }
        public TextView ProductRowItemLastModification { get; set; }
        public TextView ProductRowItemCount { get; set; }
        public ImageButton ProductRowItemInfo { get; set; }
        public ImageButton ProductRowItemEdit { get; set; }

        public ProductAdapterViewHolder(View itemView) : base(itemView)
        {
            ProductRowItemImage = itemView.FindViewById<ImageView>(Resource.Id.ProductRowItemImage);
            ProductRowItemName = itemView.FindViewById<TextView>(Resource.Id.ProductRowItemName);
            ProductRowItemLastModification = itemView.FindViewById<TextView>(Resource.Id.ProductRowItemLastModification);
            ProductRowItemCount = itemView.FindViewById<TextView>(Resource.Id.ProductRowItemCount);
            ProductRowItemInfo = itemView.FindViewById<ImageButton>(Resource.Id.ProductRowItemInfo);
            ProductRowItemEdit = itemView.FindViewById<ImageButton>(Resource.Id.ProductRowItemEdit);

            ProductRowItemInfo.Tag = this;
            ProductRowItemEdit.Tag = this;
        }
    }
}