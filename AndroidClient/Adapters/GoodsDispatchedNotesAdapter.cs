using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Models;
using Client.Providers;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class GoodsDispatchedNotesAdapter : BaseRecyclerViewAdapter<Models.GoodsDispatchedNote, GoodsDispatchedNotesViewHolder>
    {
        public ViewStates DeleteVisibility;
        public ViewStates ReadVisibility;
        public ColorConditionalStyleProvider _styleProvider;

        public GoodsDispatchedNotesAdapter(Context context, IRoleManager roleManager)
            : base(context, roleManager, Resource.Layout.GoodsDispatchedNotesRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.GoodsDispatchedNoteRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            ReadVisibility = roleManager.Permissions.ContainsKey(Resource.Id.GoodsDispatchedNoteRowItemInfo) ? ViewStates.Visible : ViewStates.Invisible;
            _styleProvider = ColorConditionalStyleProvider.Instance;
        }

        public override void BindItemToViewHolder(GoodsDispatchedNote item, GoodsDispatchedNotesViewHolder viewHolder)
        {
            viewHolder.GoodsDispatchedNoteRowItemDocumentId.Text = item.DocumentId;
            viewHolder.GoodsDispatchedNoteRowItemInvoiceId.Text = item.Invoice.DocumentId;

            _styleProvider
                .Execute(item.LastModifiedAt,
                viewHolder.GoodsDispatchedNoteRowItemDocumentId);

            viewHolder.GoodsDispatchedNoteRowItemInfo.SetOnClickListener(IOnClickListener);
            viewHolder.GoodsDispatchedNoteRowItemDelete.SetOnClickListener(IOnClickListener);

            viewHolder.GoodsDispatchedNoteRowItemInfo.Visibility = ReadVisibility;
            viewHolder.GoodsDispatchedNoteRowItemDelete.Visibility = DeleteVisibility;
        }

        public override GoodsDispatchedNotesViewHolder CreateViewHolder(View view) => new GoodsDispatchedNotesViewHolder(view);
    }
}