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
using Client.ViewHolders;
using Common.DTO;

namespace Client.Adapters
{

    public class InvoiceRowItemAdapter : BaseRecyclerViewAdapter<Models.Invoice, InvoiceRowItemViewHolder>
    {
        public InvoiceRowItemAdapter(Context context) : base(context, Resource.Layout.InvoiceRowItem)
        {
        }

        public override void BindItemToViewHolder(Models.Invoice item, InvoiceRowItemViewHolder viewHolder)
        {
            viewHolder.InvoiceRowItemAuthor.Text = item.Author;
            viewHolder.InvoiceRowItemDocumentId.Text = item.DocumentId;
            viewHolder.InvoiceRowItemIssueDate.Text = item.IssueDate.ToShortDateString();
            viewHolder.InvoiceRowItemInvoiceType.Text = item.InvoiceType.ToString();
        }

        public override InvoiceRowItemViewHolder CreateViewHolder(View view) => new InvoiceRowItemViewHolder(view);
    }
}