using System;
using Android.Content;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Listeners;
using Client.Models;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class ProductAttributesEditAdapter : BaseArrayAdapter<Models.ProductAttribute>
    {
        public ProductAttributesEditAdapter(
            Context context,
            int resourceId = Resource.Layout.ProductAttributesEditRowItem
            ) : base(context, resourceId)
        {
            AttributeAdapter = new BaseArrayAdapter<Models.Attribute>(Context);
        }

        public IAfterTextChangedListener IAfterTextChangedListener;
        public IOnItemClickListener OnItemClickListener;
        public BaseArrayAdapter<Models.Attribute> AttributeAdapter { get; set; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ProductAttributesEditRowItemViewHolder holder = null;
            var item = GetItem(position);

            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
                holder = new ProductAttributesEditRowItemViewHolder(convertView);
            }
            else
            {
                holder = (ProductAttributesEditRowItemViewHolder)convertView.Tag;
            }

            holder.ProductAttributeEditRowItemName.AfterTextChanged -= AfterTextChanged;
            holder.ProductAttributeEditRowItemValue.AfterTextChanged -= AfterTextChanged;
            holder.ProductAttributeEditRowItemName.Text = item.Attribute.Name;
            holder.ProductAttributeEditRowItemValue.Text = item.Value;            
            holder.ProductAttributeEditRowItemName.AfterTextChanged += AfterTextChanged;
            holder.ProductAttributeEditRowItemValue.AfterTextChanged += AfterTextChanged;
            holder.ProductAttributeEditRowItemName.ItemClick += ItemClick;
            holder.ProductAttributeEditRowItemDelete.SetOnClickListener(IOnClickListener);
            holder.ProductAttributeEditRowItemName.Adapter = AttributeAdapter;
            holder.ProductAttributeEditRowItemName.Threshold = 1;

            holder.ProductAttributeEditRowItemDelete.Tag = item;
            holder.ProductAttributeEditRowItemValue.Tag = item;
            holder.ProductAttributeEditRowItemName.Tag = item;

            convertView.Tag = holder;

            return convertView;
        }

        private void ItemClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var item = eventArgs.Parent.GetItemAtPosition(eventArgs.Position);
            OnItemClickListener.ItemClick((View)sender, item);
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            IAfterTextChangedListener?.AfterTextChanged((EditText)sender, eventArgs.Editable.ToString());
        }
    }
}