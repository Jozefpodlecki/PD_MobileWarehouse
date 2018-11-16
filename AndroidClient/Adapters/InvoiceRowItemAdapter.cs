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
using Client.ViewHolders;

namespace Client.Adapters
{
    public class InvoiceRowItem
    {
        public string InvoiceId { get; set; }
        public decimal Price { get; set; }
    }

    public class InvoiceRowItemAdapter : RecyclerView.Adapter
    {
        private List<InvoiceRowItem> _items { get; set; }

        public InvoiceRowItemAdapter()
        {
        }

        public override int ItemCount => _items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new InvoiceRowItemViewHolder(parent);
        }
    }
}