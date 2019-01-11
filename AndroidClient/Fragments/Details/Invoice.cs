using Android.OS;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Common;

namespace Client.Fragments.Details
{
    public class Invoice : BaseDetailsFragment<Models.Invoice>
    {
        private InvoiceDetailsProductRowItemAdapter _invoiceDetailsProductRowItemAdapter;
        public TextView InvoiceDetailsDocumentId { get; set; }
        public TextView InvoiceDetailsIssueDate { get; set; }
        public TextView InvoiceDetailsInvoiceType { get; set; }
        public TextView InvoiceDetailsPaymentMethod { get; set; }
        public TextView InvoiceDetailsTotal { get; set; }
        public TextView InvoiceDetailsVAT { get; set; }
        public ListView InvoiceDetailsProducts { get; set; }

        public Invoice() : base(Resource.Layout.InvoiceDetails)
        {
        }

        public override void OnBindElements(View view)
        {
            InvoiceDetailsDocumentId = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsDocumentId);
            InvoiceDetailsIssueDate = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsIssueDate);
            InvoiceDetailsInvoiceType = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsInvoiceType);
            InvoiceDetailsPaymentMethod = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsPaymentMethod);
            InvoiceDetailsProducts = view.FindViewById<ListView>(Resource.Id.InvoiceDetailsProducts);
            InvoiceDetailsTotal = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsTotal);
            InvoiceDetailsVAT = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsVAT);

            InvoiceDetailsDocumentId.Text = Entity.DocumentId;
            InvoiceDetailsIssueDate.Text = Entity.IssueDate.ToShortDateString();
            InvoiceDetailsInvoiceType.Text = GetString(Entity.InvoiceType.GetFullName());
            InvoiceDetailsPaymentMethod.Text = GetString(Entity.PaymentMethod.GetFullName());
            InvoiceDetailsTotal.Text = Entity.Total.ToString("C");
            InvoiceDetailsVAT.Text = Entity.VAT.ToString("C");

            _invoiceDetailsProductRowItemAdapter = new InvoiceDetailsProductRowItemAdapter(Context);
            InvoiceDetailsProducts.Adapter = _invoiceDetailsProductRowItemAdapter;

            _invoiceDetailsProductRowItemAdapter.UpdateList(Entity.Products);
        }
    }
}