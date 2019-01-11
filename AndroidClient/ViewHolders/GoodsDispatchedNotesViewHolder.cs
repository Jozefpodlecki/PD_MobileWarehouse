using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class GoodsDispatchedNotesViewHolder : RecyclerView.ViewHolder
    {
        public TextView GoodsDispatchedNoteRowItemDocumentId { get; set; }
        public TextView GoodsDispatchedNoteRowItemInvoiceId { get; set; }
        public ImageButton GoodsDispatchedNoteRowItemInfo { get; set; }
        public ImageButton GoodsDispatchedNoteRowItemDelete { get; set; }

        public GoodsDispatchedNotesViewHolder(View itemView) : base(itemView)
        {
            GoodsDispatchedNoteRowItemDocumentId = itemView.FindViewById<TextView>(Resource.Id.GoodsDispatchedNoteRowItemDocumentId);
            GoodsDispatchedNoteRowItemInvoiceId = itemView.FindViewById<TextView>(Resource.Id.GoodsDispatchedNoteRowItemInvoiceId);
            GoodsDispatchedNoteRowItemInfo = itemView.FindViewById<ImageButton>(Resource.Id.GoodsDispatchedNoteRowItemInfo);
            GoodsDispatchedNoteRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.GoodsDispatchedNoteRowItemDelete);
        }
    }
}