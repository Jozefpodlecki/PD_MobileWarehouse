using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Details
{
    public class GoodsReceivedNote : BaseDetailsFragment<Models.GoodsReceivedNote>
    {
        public TextView GoodsReceivedNoteDetailsInvoiceId { get; set; }
        public TextView GoodsReceivedNoteDetailsDocumentId { get; set; }

        public GoodsReceivedNote() : base(Resource.Layout.GoodsReceivedNoteDetails)
        {
        }

        public override void OnBindElements(View view)
        {
            GoodsReceivedNoteDetailsInvoiceId = view.FindViewById<TextView>(Resource.Id.GoodsReceivedNoteDetailsInvoiceId);
            GoodsReceivedNoteDetailsDocumentId = view.FindViewById<TextView>(Resource.Id.GoodsReceivedNoteDetailsDocumentId);
                 
            GoodsReceivedNoteDetailsInvoiceId.Text = Entity.Invoice.DocumentId;
            GoodsReceivedNoteDetailsDocumentId.Text = Entity.DocumentId;
        }
    }
}