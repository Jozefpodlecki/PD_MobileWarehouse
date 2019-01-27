using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class GoodsReceivedNotesViewHolder : RecyclerView.ViewHolder
    {
        public TextView GoodsReceivedNoteRowItemDocumentId { get; set; }
        public TextView GoodsReceivedNoteRowItemInvoiceId { get; set; }
        public ImageButton GoodsReceivedNoteRowItemInfo { get; set; }
        public ImageButton GoodsReceivedNoteRowItemDelete { get; set; }

        public GoodsReceivedNotesViewHolder(View itemView) : base(itemView)
        {
            GoodsReceivedNoteRowItemDocumentId = itemView.FindViewById<TextView>(Resource.Id.GoodsReceivedNoteRowItemDocumentId);
            GoodsReceivedNoteRowItemInvoiceId = itemView.FindViewById<TextView>(Resource.Id.GoodsReceivedNoteRowItemInvoiceId);
            GoodsReceivedNoteRowItemInfo = itemView.FindViewById<ImageButton>(Resource.Id.GoodsReceivedNoteRowItemInfo);
            GoodsReceivedNoteRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.GoodsReceivedNoteRowItemDelete);

            GoodsReceivedNoteRowItemInfo.Tag = this;
            GoodsReceivedNoteRowItemDelete.Tag = this;
        }
    }
}