using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class ProductAttributesDetailsRowItemViewHolder : Java.Lang.Object
    {
        public TextView ProductAttributeDetailsRowItemName { get; set; }
        public TextView ProductAttributeDetailsRowItemValue { get; set; }

        public ProductAttributesDetailsRowItemViewHolder(View view)
        {
            ProductAttributeDetailsRowItemName = view.FindViewById<TextView>(Resource.Id.ProductAttributeDetailsRowItemName);
            ProductAttributeDetailsRowItemValue = view.FindViewById<TextView>(Resource.Id.ProductAttributeDetailsRowItemValue);
        }
    }
}