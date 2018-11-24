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
using Common.DTO;

namespace Client.Adapters
{

    public class InvoiceRowItemAdapter : RecyclerView.Adapter
    {
        private List<Invoice> _items { get; set; }

        public InvoiceRowItemAdapter()
        {
            _items = new List<Invoice>();
        }

        public void UpdateList(List<Invoice> items)
        {
            _items = items;
            NotifyDataSetChanged();
        }

        public override int ItemCount => _items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as InvoiceRowItemViewHolder;

            var item = _items[position];

            viewHolder.InvoiceRowItemAuthor.Text = item.Author;
            viewHolder.InvoiceRowItemDocumentId.Text = item.DocumentId;
            viewHolder.InvoiceRowItemIssueDate.Text = item.IssueDate.ToShortDateString();
            viewHolder.InvoiceRowItemInvoiceType.Text = item.InvoiceType.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.InvoiceRowItem, parent, false);

            return new InvoiceRowItemViewHolder(itemView);
        }
    }
}