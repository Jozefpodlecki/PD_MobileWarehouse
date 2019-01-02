using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Details
{
    public class GoodsDispatchedNote : BaseFragment,
        View.IOnClickListener
    {
        public TextView GoodsDispatchedNoteDetailsInvoiceId { get; set; }
        public Models.GoodsDispatchedNote Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.GoodsDispatchedNoteDetails, container, false);
            GoodsDispatchedNoteDetailsInvoiceId = view.FindViewById<TextView>(Resource.Id.GoodsDispatchedNoteDetailsInvoiceId);

            GoodsDispatchedNoteDetailsInvoiceId.PaintFlags = GoodsDispatchedNoteDetailsInvoiceId.PaintFlags | Android.Graphics.PaintFlags.UnderlineText;
            GoodsDispatchedNoteDetailsInvoiceId.SetOnClickListener(this);


            Entity = (Models.GoodsDispatchedNote)Arguments.GetParcelable(Constants.Entity);

            GoodsDispatchedNoteDetailsInvoiceId.Text = Entity.Invoice.DocumentId;

            return view;
        }

        public void OnClick(View view)
        {
            if(view.Id == Resource.Id.GoodsReceivedNoteDetailsInvoiceId)
            {

            }
        }
    }
}