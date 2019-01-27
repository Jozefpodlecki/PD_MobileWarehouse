using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.ViewHolders;
using Common;

namespace Client.Adapters
{

    public class InvoiceRowItemAdapter : BaseRecyclerViewAdapter<Models.Invoice, InvoiceRowItemViewHolder>
    {
        public ViewStates DeleteVisibility;
        public ViewStates ReadVisibility;
        private string _itemsFormat;

        public InvoiceRowItemAdapter(Context context, IRoleManager roleManager) : base(context, roleManager, Resource.Layout.InvoiceRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.InvoiceRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            ReadVisibility = roleManager.Permissions.ContainsKey(Resource.Id.InvoiceRowItemInfo) ? ViewStates.Visible : ViewStates.Invisible;

            _itemsFormat = _context.Resources.GetString(Resource.String.InvoiceEntries);
        }

        public string GetString(string identifierName)
        {
            var packageName = _context.PackageName;

            var resourceId = _context.Resources.GetIdentifier(identifierName, "string", packageName);

            if (resourceId == 0)
            {
                return identifierName;
            }

            return _context.GetString(resourceId);
        }

        public override void BindItemToViewHolder(Models.Invoice item, InvoiceRowItemViewHolder viewHolder)
        {
            viewHolder.InvoiceRowItemDocumentId.Text = item.DocumentId;
            viewHolder.InvoiceRowItemInvoiceType.Text = GetString(item.InvoiceType.GetFullName());
            viewHolder.InvoiceRowItemItems.Text = string.Format(_itemsFormat, item.Products.Count);
            viewHolder.InvoiceRowItemDelete.Visibility = DeleteVisibility;
            viewHolder.InvoiceRowItemInfo.Visibility = ReadVisibility;
            viewHolder.InvoiceRowItemDelete.SetOnClickListener(IOnClickListener);
            viewHolder.InvoiceRowItemInfo.SetOnClickListener(IOnClickListener);

            if(item.Note != null)
            {
                viewHolder.InvoiceRowItemDelete.Enabled = false;
                viewHolder.InvoiceRowItemInfo.Enabled = false;
            }
        }

        public override InvoiceRowItemViewHolder CreateViewHolder(View view) => new InvoiceRowItemViewHolder(view);
    }
}