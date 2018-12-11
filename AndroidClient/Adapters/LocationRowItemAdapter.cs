using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Client.Models;
using Client.Services;
using Client.ViewHolders;
using Common.DTO;

namespace Client.Adapters
{
    public class LocationRowItemAdapter : BaseRecyclerViewAdapter<Models.Location, LocationRowItemViewHolder>
    {
        public LocationRowItemAdapter(Context context) : base(context, Resource.Layout.LocationsRowItem)
        {
        }

        public override void BindItemToViewHolder(Models.Location item, LocationRowItemViewHolder viewHolder)
        {
            viewHolder.LocationRowItemName.Text = item.Name;
            viewHolder.LocationRowItemEdit.SetOnClickListener(IOnClickListener);
            viewHolder.LocationRowItemDelete.SetOnClickListener(IOnClickListener);
        }

        public override LocationRowItemViewHolder CreateViewHolder(View view) => new LocationRowItemViewHolder(view);

    }
}