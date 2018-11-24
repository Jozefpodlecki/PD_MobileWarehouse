using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class InvoiceRowItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView InvoiceRowItemIssueDate { get; set; }
        public TextView InvoiceRowItemDocumentId { get; set; }
        public TextView InvoiceRowItemAuthor { get; set; }
        public TextView InvoiceRowItemInvoiceType { get; set; }

        public InvoiceRowItemViewHolder(View itemView) : base(itemView)
        {
            InvoiceRowItemIssueDate = itemView.FindViewById<TextView>(Resource.Id.InvoiceRowItemIssueDate);
            InvoiceRowItemDocumentId = itemView.FindViewById<TextView>(Resource.Id.InvoiceRowItemDocumentId);
            InvoiceRowItemAuthor = itemView.FindViewById<TextView>(Resource.Id.InvoiceRowItemAuthor);
            InvoiceRowItemInvoiceType = itemView.FindViewById<TextView>(Resource.Id.InvoiceRowItemInvoiceType);
        }
    }
}