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
    public class ProductDetailsDetailsViewHolder : Java.Lang.Object
    {
        public TextView ProductDetailsDetailsRowItemLocation { get; set; }
        public TextView ProductDetailsDetailsRowItemCount { get; set; }

        public ProductDetailsDetailsViewHolder(View view)
        {
            ProductDetailsDetailsRowItemLocation = view.FindViewById<TextView>(Resource.Id.ProductDetailsDetailsRowItemLocation);
            ProductDetailsDetailsRowItemCount = view.FindViewById<TextView>(Resource.Id.ProductDetailsDetailsRowItemCount);
        }
    }
}