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
using Client.ViewHolders;

namespace Client.Adapters
{
    public class ProductDetailsDetailsAdapter : BaseArrayAdapter<Models.ProductDetail>
    {
        public ProductDetailsDetailsAdapter(
            Context context,
            int resourceId = Resource.Layout.ProductDetailsDetailsRowItem
            ) : base(context, resourceId)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ProductDetailsDetailsViewHolder holder = null;
            var item = GetItem(position);

            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
                holder = new ProductDetailsDetailsViewHolder(convertView);
                convertView.Tag = holder;
            }
            else
            {
                holder = (ProductDetailsDetailsViewHolder)convertView.Tag;
            }

            holder.ProductDetailsDetailsRowItemLocation.Text = item.Location.ToString();
            holder.ProductDetailsDetailsRowItemCount.Text = item.Count.ToString();

            return convertView;
        }
    }
}