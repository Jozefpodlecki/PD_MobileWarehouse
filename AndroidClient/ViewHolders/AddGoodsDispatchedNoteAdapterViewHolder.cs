using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class AddGoodsDispatchedNoteAdapterViewHolder : Java.Lang.Object
    {
        public TextView AddGoodsDispatchedNoteRowItemProductName { get; set; }
        public Spinner AddGoodsDispatchedNoteRowItemLocation { get; set; }
        public int Position { get; set; }

        public AddGoodsDispatchedNoteAdapterViewHolder(View view)
        {
            AddGoodsDispatchedNoteRowItemProductName = view.FindViewById<TextView>(Resource.Id.AddGoodsReceivedNoteRowItemProductName);
            AddGoodsDispatchedNoteRowItemLocation = view.FindViewById<Spinner>(Resource.Id.AddGoodsReceivedNoteRowItemLocation);

            AddGoodsDispatchedNoteRowItemProductName.Tag = this;
        }
    }
}