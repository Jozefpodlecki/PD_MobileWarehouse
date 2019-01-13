using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Models;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class GoodsReceivedNotesAdapter
        : BaseRecyclerViewAdapter<Models.GoodsReceivedNote, GoodsReceivedNotesViewHolder>
    {

        public GoodsReceivedNotesAdapter(Context context, IRoleManager roleManager)
            : base(context, roleManager, Resource.Layout.GoodsReceivedNotesRowItem)
        {
        }

        public override void BindItemToViewHolder(GoodsReceivedNote item, GoodsReceivedNotesViewHolder viewHolder)
        {
            viewHolder.GoodsReceivedNoteRowItemDocumentId.Text = item.DocumentId;
            viewHolder.GoodsReceivedNoteRowItemInvoiceId.Text = item.Invoice.DocumentId;
            viewHolder.GoodsReceivedNoteRowItemInfo.SetOnClickListener(IOnClickListener);
            viewHolder.GoodsReceivedNoteRowItemDelete.SetOnClickListener(IOnClickListener);
        }

        public override GoodsReceivedNotesViewHolder CreateViewHolder(View view) => new GoodsReceivedNotesViewHolder(view);
    }
}