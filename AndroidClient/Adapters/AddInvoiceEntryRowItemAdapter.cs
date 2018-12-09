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
using Client.Helpers;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class AddInvoiceEntryRowItemAdapter : BaseArrayAdapter<Models.Entry>,
        View.IOnClickListener
    {
        public AddInvoiceEntryRowItemAdapter(
            Context context,
            int resourceId = Resource.Layout.AddInvoiceEntryRowItem
            ) : base(context, resourceId)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
            }

            var item = GetItem(position);

            var viewHolder = new AddInvoiceEntryRowItemViewHolder(convertView);
            convertView.Tag = new JavaObjectWrapper<AddInvoiceEntryRowItemViewHolder>(viewHolder);
            viewHolder.AddInvoiceProductRemove.SetOnClickListener(this);
            viewHolder.AddInvoiceProductRemove.Tag = new JavaObjectWrapper<Command>(new Command { Type = CommandType.Remove, Data = position });
            viewHolder.AddInvoiceProductBarcode.SetOnClickListener(this);
            viewHolder.AddInvoiceProductBarcode.Tag = new JavaObjectWrapper<Command>(new Command { Type = CommandType.Barcode });
            viewHolder.AddInvoiceProductQRCode.SetOnClickListener(this);
            viewHolder.AddInvoiceProductQRCode.Tag = new JavaObjectWrapper<Command>(new Command { Type = CommandType.QRCode });

            viewHolder.AddInvoiceProductName.Tag = item;
            viewHolder.AddInvoiceProductPrice.Tag = item;
            viewHolder.AddInvoiceProductCount.Tag = item;
            viewHolder.AddInvoiceProductVAT.Tag = item;
            viewHolder.AddInvoiceProductName.AfterTextChanged += (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
            viewHolder.AddInvoiceProductPrice.AfterTextChanged += (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
            viewHolder.AddInvoiceProductCount.AfterTextChanged += (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());
            viewHolder.AddInvoiceProductVAT.AfterTextChanged += (view, eventArgs) => OnEditText((EditText)view, eventArgs.Editable.ToString());

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
                    item.Price = decimal.Parse(text);
                    break;
                case Resource.Id.AddInvoiceProductCount:
                    if (!int.TryParse(text, out int intValue))
                    {
                        view.SetError("Wrong number", null);
                    }
                    item.Count = intValue;
                    break;
                case Resource.Id.AddInvoiceProductVAT:
                    item.VAT = decimal.Parse(text);
                    break;
            }
        }

        internal class Command : Java.Lang.Object
        {
            public CommandType Type { get; set; }
            public object Data { get; set; }
        }

        internal enum CommandType : byte
        {
            Remove,
            Barcode,
            QRCode
        }

        public void OnClick(View view)
        {
            var command = ((JavaObjectWrapper<Command>)view.Tag).Data;

            switch (command.Type)
            {
                case CommandType.Remove:
                    Remove(GetItem((int)command.Data));
                    NotifyDataSetChanged();
                    break;
                case CommandType.Barcode:
                    break;
                case CommandType.QRCode:
                    break;
            }
        }
    }
}