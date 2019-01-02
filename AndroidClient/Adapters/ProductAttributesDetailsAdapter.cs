using Android.Content;
using Android.Views;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class ProductAttributesDetailsAdapter : BaseArrayAdapter<Models.ProductAttribute>
    {
        public ProductAttributesDetailsAdapter(
            Context context,
            int resourceId = Resource.Layout.ProductAttributesDetailsRowItem
            ) : base(context, resourceId)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ProductAttributesDetailsRowItemViewHolder holder = null;
            var item = GetItem(position);

            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
                holder = new ProductAttributesDetailsRowItemViewHolder(convertView);
            }
            else
            {
                holder = (ProductAttributesDetailsRowItemViewHolder)convertView.Tag;
            }

            holder.ProductAttributeDetailsRowItemName.Text = item.Attribute.Name;
            holder.ProductAttributeDetailsRowItemValue.Text = item.Value;

            return convertView;
        }
    }
}