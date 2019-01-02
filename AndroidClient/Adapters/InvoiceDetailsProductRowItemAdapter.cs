using Android.Content;
using Android.Views;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class InvoiceDetailsProductRowItemAdapter : BaseArrayAdapter<Models.Entry>
    {
        public InvoiceDetailsProductRowItemAdapter(
            Context context
            ) 
            : base(context, Resource.Layout.InvoiceDetailsProductRowItem)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            InvoiceDetailsProductRowItemAdapterViewHolder viewHolder = null;

            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
                viewHolder = new InvoiceDetailsProductRowItemAdapterViewHolder(convertView);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (InvoiceDetailsProductRowItemAdapterViewHolder)convertView.Tag;
            }

            var item = GetItem(position);

            viewHolder.InvoiceDetailsProductRowItemName.Text = item.Name;
            viewHolder.InvoiceDetailsProductRowItemPrice.Text = item.Price.ToString("C");
            viewHolder.InvoiceDetailsProductRowItemVAT.Text = item.VAT.ToString("P");
            viewHolder.InvoiceDetailsProductRowItemCount.Text = item.Count.ToString();

            return convertView;
        }
        
    }
}