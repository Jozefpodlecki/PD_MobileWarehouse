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
    public class InvoiceDetailsProductRowItemAdapterViewHolder : Java.Lang.Object
    {
        public TextView InvoiceDetailsProductRowItemName { get; set; }
        public TextView InvoiceDetailsProductRowItemPrice { get; set; }
        public TextView InvoiceDetailsProductRowItemVAT { get; set; }
        public TextView InvoiceDetailsProductRowItemCount { get; set; }

        public InvoiceDetailsProductRowItemAdapterViewHolder(View view)
        {
            InvoiceDetailsProductRowItemName = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsProductRowItemName);
            InvoiceDetailsProductRowItemPrice = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsProductRowItemPrice);
            InvoiceDetailsProductRowItemVAT = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsProductRowItemVAT);
            InvoiceDetailsProductRowItemCount = view.FindViewById<TextView>(Resource.Id.InvoiceDetailsProductRowItemCount);
        }
    }
}