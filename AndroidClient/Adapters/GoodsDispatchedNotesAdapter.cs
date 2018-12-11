using Android.Content;
using Android.Views;
using Client.Models;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class GoodsDispatchedNotesAdapter : BaseRecyclerViewAdapter<Models.GoodsDispatchedNote, GoodsDispatchedNotesViewHolder>
    {
        public GoodsDispatchedNotesAdapter(Context context) : base(context, Resource.Layout.GoodsDispatchedNotesRowItem)
        {
        }

        public override void BindItemToViewHolder(GoodsDispatchedNote item, GoodsDispatchedNotesViewHolder viewHolder)
        {
            
        }

        public override GoodsDispatchedNotesViewHolder CreateViewHolder(View view) => new GoodsDispatchedNotesViewHolder(view);
    }
}