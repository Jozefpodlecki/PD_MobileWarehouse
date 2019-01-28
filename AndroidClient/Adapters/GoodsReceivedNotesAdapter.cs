using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Models;
using Client.Providers;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class GoodsReceivedNotesAdapter
        : BaseRecyclerViewAdapter<Models.GoodsReceivedNote, GoodsReceivedNotesViewHolder>
    {
        public ViewStates DeleteVisibility;
        public ViewStates ReadVisibility;
        public ColorConditionalStyleProvider _styleProvider;

        public GoodsReceivedNotesAdapter(Context context, IRoleManager roleManager)
            : base(context, roleManager, Resource.Layout.GoodsReceivedNotesRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.GoodsReceivedNoteRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            ReadVisibility = roleManager.Permissions.ContainsKey(Resource.Id.GoodsReceivedNoteRowItemInfo) ? ViewStates.Visible : ViewStates.Invisible;
            _styleProvider = ColorConditionalStyleProvider.Instance;
        }

        public override void BindItemToViewHolder(GoodsReceivedNote item, GoodsReceivedNotesViewHolder viewHolder)
        {
            viewHolder.GoodsReceivedNoteRowItemDocumentId.Text = item.DocumentId;
            viewHolder.GoodsReceivedNoteRowItemInvoiceId.Text = item.Invoice.DocumentId;

            _styleProvider
                .Execute(item.LastModifiedAt,
                viewHolder.GoodsReceivedNoteRowItemDocumentId);

            viewHolder.GoodsReceivedNoteRowItemInfo.SetOnClickListener(IOnClickListener);
            viewHolder.GoodsReceivedNoteRowItemDelete.SetOnClickListener(IOnClickListener);

            viewHolder.GoodsReceivedNoteRowItemInfo.Visibility = ReadVisibility;
            viewHolder.GoodsReceivedNoteRowItemDelete.Visibility = DeleteVisibility;
        }

        public override GoodsReceivedNotesViewHolder CreateViewHolder(View view) => new GoodsReceivedNotesViewHolder(view);
    }
}