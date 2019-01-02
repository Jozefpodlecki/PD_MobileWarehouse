using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class ProductAttributesEditRowItemViewHolder : Java.Lang.Object
    {
        public AutoCompleteTextView ProductAttributeEditRowItemName { get; set; }
        public TextInputEditText ProductAttributeEditRowItemValue { get; set; }
        public ImageButton ProductAttributeEditRowItemDelete { get; set; }

        public ProductAttributesEditRowItemViewHolder(View view)
        {
            ProductAttributeEditRowItemName = view.FindViewById<AutoCompleteTextView>(Resource.Id.ProductAttributeEditRowItemName);
            ProductAttributeEditRowItemValue = view.FindViewById<TextInputEditText>(Resource.Id.ProductAttributeEditRowItemValue);
            ProductAttributeEditRowItemDelete = view.FindViewById<ImageButton>(Resource.Id.ProductAttributeEditRowItemDelete);
        }
    }
}