using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class InvoiceRowItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView InvoiceRowItemDocumentId { get; set; }
        public TextView InvoiceRowItemInvoiceType { get; set; }
        public TextView InvoiceRowItemItems { get; set; }
        public ImageButton InvoiceRowItemInfo { get; set; }
        public ImageButton InvoiceRowItemDelete { get; set; }

        public InvoiceRowItemViewHolder(View itemView) : base(itemView)
        {
            InvoiceRowItemDocumentId = itemView.FindViewById<TextView>(Resource.Id.InvoiceRowItemDocumentId);
            InvoiceRowItemInvoiceType = itemView.FindViewById<TextView>(Resource.Id.InvoiceRowItemInvoiceType);
            InvoiceRowItemItems = itemView.FindViewById<TextView>(Resource.Id.InvoiceRowItemItems);
            InvoiceRowItemInfo = itemView.FindViewById<ImageButton>(Resource.Id.InvoiceRowItemInfo);
            InvoiceRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.InvoiceRowItemDelete);

            InvoiceRowItemInfo.Tag = this;
            InvoiceRowItemDelete.Tag = this;
        }
    }
}