using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class AddInvoiceEntryRowItemViewHolder
    {
        public EditText AddInvoiceProductName { get; set; }
        public ImageButton AddInvoiceProductBarcode { get; set; }
        public ImageButton AddInvoiceProductQRCode { get; set; }
        public EditText AddInvoiceProductPrice { get; set; }
        public EditText AddInvoiceProductCount { get; set; }
        public EditText AddInvoiceProductVAT { get; set; }
        public ImageButton AddInvoiceProductRemove { get; set; }

        public AddInvoiceEntryRowItemViewHolder(View view)
        {
            AddInvoiceProductName = view.FindViewById<EditText>(Resource.Id.AddInvoiceProductName);
            AddInvoiceProductBarcode = view.FindViewById<ImageButton>(Resource.Id.AddInvoiceProductBarcode);
            AddInvoiceProductQRCode = view.FindViewById<ImageButton>(Resource.Id.AddInvoiceProductQRCode);
            AddInvoiceProductPrice = view.FindViewById<EditText>(Resource.Id.AddInvoiceProductPrice);
            AddInvoiceProductCount = view.FindViewById<EditText>(Resource.Id.AddInvoiceProductCount);
            AddInvoiceProductVAT = view.FindViewById<EditText>(Resource.Id.AddInvoiceProductVAT);
            AddInvoiceProductRemove = view.FindViewById<ImageButton>(Resource.Id.AddInvoiceProductRemove);
        }
    }
}