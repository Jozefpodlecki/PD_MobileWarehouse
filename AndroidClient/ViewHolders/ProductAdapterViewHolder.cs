using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class ProductAdapterViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView Product { get; set; }
        public TextView LastModification { get; set; }
        public ImageButton ProductRowItemInfo { get; set; }

        public ProductAdapterViewHolder(View itemView) : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.ProductRowItemImage);
            Product = itemView.FindViewById<TextView>(Resource.Id.ProductRowItemName);
            LastModification = itemView.FindViewById<TextView>(Resource.Id.ProductRowItemLastModification);
            ProductRowItemInfo = itemView.FindViewById<ImageButton>(Resource.Id.ProductRowItemInfo);

            ProductRowItemInfo.Tag = this;
        }
    }
}