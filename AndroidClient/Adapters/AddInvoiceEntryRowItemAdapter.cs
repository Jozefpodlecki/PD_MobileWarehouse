using Android.Content;
using Android.Views;
using Android.Widget;
using Client.Helpers;
using Client.ViewHolders;
using System.Globalization;
using static Android.Views.View;

namespace Client.Adapters
{
    public class AddInvoiceEntryRowItemAdapter : BaseArrayAdapter<Models.Entry>,
        IOnFocusChangeListener
    {
        private string _invalidVatError;
        private string _invalidNumberError;

        public AddInvoiceEntryRowItemAdapter(
            Context context,
            int resourceId = Resource.Layout.AddInvoiceEntryRowItem
            ) : base(context, resourceId)
        {
            _invalidVatError = context.Resources.GetString(Resource.String.InvalidVAT);
            _invalidNumberError = context.Resources.GetString(Resource.String.InvalidNumber);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            AddInvoiceEntryRowItemViewHolder viewHolder = null;

            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
                viewHolder = new AddInvoiceEntryRowItemViewHolder(convertView);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (AddInvoiceEntryRowItemViewHolder)convertView.Tag;
                viewHolder.AddInvoiceProductRemove.SetOnClickListener(null);
                viewHolder.AddInvoiceProductBarcode.SetOnClickListener(null);
                viewHolder.AddInvoiceProductQRCode.SetOnClickListener(null);
                viewHolder.AddInvoiceProductName.AfterTextChanged -= (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
                viewHolder.AddInvoiceProductPrice.AfterTextChanged -= (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
                viewHolder.AddInvoiceProductCount.AfterTextChanged -= (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
                viewHolder.AddInvoiceProductVAT.AfterTextChanged -= (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
            }

            var item = GetItem(position);

            viewHolder.AddInvoiceProductRemove.Tag = item;
            viewHolder.AddInvoiceProductName.Tag = item;
            viewHolder.AddInvoiceProductPrice.Tag = item;
            viewHolder.AddInvoiceProductCount.Tag = item;
            viewHolder.AddInvoiceProductVAT.Tag = item;

            viewHolder.AddInvoiceProductRemove.SetOnClickListener(IOnClickListener);
            viewHolder.AddInvoiceProductBarcode.SetOnClickListener(IOnClickListener);
            viewHolder.AddInvoiceProductQRCode.SetOnClickListener(IOnClickListener);
            viewHolder.AddInvoiceProductName.AfterTextChanged += (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
            viewHolder.AddInvoiceProductPrice.AfterTextChanged += (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
            viewHolder.AddInvoiceProductCount.AfterTextChanged += (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
            viewHolder.AddInvoiceProductVAT.AfterTextChanged += (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
            viewHolder.AddInvoiceProductPrice.OnFocusChangeListener = this;
            viewHolder.AddInvoiceProductCount.OnFocusChangeListener = this;
            viewHolder.AddInvoiceProductVAT.OnFocusChangeListener = this;

            return convertView;
        }



        public void OnEditText(EditText view, string text)
        {
            var item = view.Tag as Models.Entry;
            switch (view.Id)
            {
                case Resource.Id.AddInvoiceProductName:
                    item.Name = text;
                    break;
                case Resource.Id.AddInvoiceProductPrice:
                    if(!decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalPriceValue))
                    {
                        break;
                    }
                    item.Price = decimalPriceValue;
                    break;
                case Resource.Id.AddInvoiceProductCount:
                    if (!int.TryParse(text, out int intValue))
                    {
                        break;
                    }
                    item.Count = intValue;
                    break;
                case Resource.Id.AddInvoiceProductVAT:
                    if (!decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalVATValue))
                    {
                        break;
                    }
                    if(decimalVATValue >= 1M || decimalVATValue <= 0M)
                    {
                        break;
                    }
                    item.VAT = decimalVATValue;
                    break;
            }
        }

        public void OnFocusChange(View view, bool hasFocus)
        {
            if (hasFocus)
            {
                return;
            }

            var editText = (EditText)view;
            var text = editText.Text;
            var item = view.Tag as Models.Entry;

            switch (view.Id)
            {
                case Resource.Id.AddInvoiceProductName:
                    item.Name = text;
                    break;
                case Resource.Id.AddInvoiceProductPrice:
                    if (!decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalPriceValue))
                    {
                        editText.Error = _invalidNumberError;
                        break;
                    }
                    item.Price = decimalPriceValue;
                    break;
                case Resource.Id.AddInvoiceProductCount:
                    if (!int.TryParse(text, out int intValue))
                    {
                        editText.Error = _invalidNumberError;
                        break;
                    }
                    item.Count = intValue;
                    break;
                case Resource.Id.AddInvoiceProductVAT:
                    if (!decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalVATValue))
                    {
                        editText.Error = _invalidNumberError;
                        break;
                    }
                    if (decimalVATValue >= 1M || decimalVATValue <= 0M)
                    {
                        editText.Error = _invalidVatError;
                    }
                    item.VAT = decimalVATValue;
                    break;
            }
        }
    }
}