using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Models;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class GoodsDispatchedNotesAdapter : BaseRecyclerViewAdapter<Models.GoodsDispatchedNote, GoodsDispatchedNotesViewHolder>
    {
        public GoodsDispatchedNotesAdapter(Context context, RoleManager roleManager) : base(context, roleManager, Resource.Layout.GoodsDispatchedNotesRowItem)
        {
        }

        public override void BindItemToViewHolder(GoodsDispatchedNote item, GoodsDispatchedNotesViewHolder viewHolder)
        {
            
        }

        public override GoodsDispatchedNotesViewHolder CreateViewHolder(View view) => new GoodsDispatchedNotesViewHolder(view);
    }
}