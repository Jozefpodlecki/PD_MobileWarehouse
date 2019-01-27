using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Details
{
    public class GoodsDispatchedNote : BaseDetailsFragment<Models.GoodsDispatchedNote>,
        View.IOnClickListener
    {
        public TextView GoodsDispatchedNoteDetailsInvoiceId { get; set; }
        public TextView GoodsDispatchedNoteDetailsDocumentId { get; set; }

        public GoodsDispatchedNote() : base(Resource.Layout.GoodsDispatchedNoteDetails)
        {
        }

        public override void OnBindElements(View view)
        {
            GoodsDispatchedNoteDetailsInvoiceId = view.FindViewById<TextView>(Resource.Id.GoodsDispatchedNoteDetailsInvoiceId);
            GoodsDispatchedNoteDetailsDocumentId = view.FindViewById<TextView>(Resource.Id.GoodsDispatchedNoteDetailsDocumentId);

            GoodsDispatchedNoteDetailsInvoiceId.PaintFlags = GoodsDispatchedNoteDetailsInvoiceId.PaintFlags | Android.Graphics.PaintFlags.UnderlineText;
            GoodsDispatchedNoteDetailsInvoiceId.SetOnClickListener(this);
            
            GoodsDispatchedNoteDetailsInvoiceId.Text = Entity.Invoice.DocumentId;
            GoodsDispatchedNoteDetailsDocumentId.Text = Entity.DocumentId;
        }

        public void OnClick(View view)
        {
            if(view.Id == Resource.Id.GoodsReceivedNoteDetailsInvoiceId)
            {
            }
        }
    }
}