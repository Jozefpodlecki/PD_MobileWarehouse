﻿using Android.Content;
using Android.Views;
using Client.Models;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class GoodsReceivedNotesAdapter : BaseRecyclerViewAdapter<Models.GoodsReceivedNote, GoodsReceivedNotesViewHolder>
    {

        public GoodsReceivedNotesAdapter(Context context) : base(context, Resource.Layout.GoodsReceivedNotesRowItem)
        {
        }

        public override void BindItemToViewHolder(GoodsReceivedNote item, GoodsReceivedNotesViewHolder viewHolder)
        {
            
        }

        public override GoodsReceivedNotesViewHolder CreateViewHolder(View view) => new GoodsReceivedNotesViewHolder(view);
    }
}