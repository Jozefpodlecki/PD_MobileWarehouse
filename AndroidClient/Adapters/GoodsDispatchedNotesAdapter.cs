using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Models;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class GoodsDispatchedNotesAdapter : BaseRecyclerViewAdapter<Models.GoodsDispatchedNote, GoodsDispatchedNotesViewHolder>
    {
        public GoodsDispatchedNotesAdapter(Context context, IRoleManager roleManager)
            : base(context, roleManager, Resource.Layout.GoodsDispatchedNotesRowItem)
        {
        }

        public override void BindItemToViewHolder(GoodsDispatchedNote item, GoodsDispatchedNotesViewHolder viewHolder)
        {
            viewHolder.GoodsDispatchedNoteRowItemDocumentId.Text = item.DocumentId;
            viewHolder.GoodsDispatchedNoteRowItemInvoiceId.Text = item.Invoice.DocumentId;
            viewHolder.GoodsDispatchedNoteRowItemInfo.SetOnClickListener(IOnClickListener);
            viewHolder.GoodsDispatchedNoteRowItemDelete.SetOnClickListener(IOnClickListener);
        }

        public override GoodsDispatchedNotesViewHolder CreateViewHolder(View view) => new GoodsDispatchedNotesViewHolder(view);
    }
}