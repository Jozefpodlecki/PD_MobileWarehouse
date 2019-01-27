using Android.Content;
using Android.Graphics;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Providers;
using Client.ViewHolders;
using System;
using System.Linq;

namespace Client.Adapters
{
    public class ProductAdapter : BaseRecyclerViewAdapter<Models.Product, ProductAdapterViewHolder>
    {
        public ViewStates ReadVisibility;
        public ViewStates EditVisibility;
        public ColorConditionalStyleProvider _styleProvider;
        private string _itemsFormat;
        private string _lastModificationFormat;

        public ProductAdapter(Context context, IRoleManager roleManager) 
            : base(context, roleManager, Resource.Layout.ProductRowItem)
        {
            ReadVisibility = roleManager.Permissions.ContainsKey(Resource.Id.ProductRowItemInfo) ? ViewStates.Visible : ViewStates.Invisible;
            EditVisibility = roleManager.Permissions.ContainsKey(Resource.Id.ProductRowItemEdit) ? ViewStates.Visible : ViewStates.Invisible;
            _styleProvider = ColorConditionalStyleProvider.Instance;
            _itemsFormat = _context.Resources.GetString(Resource.String.InvoiceEntries);
            _lastModificationFormat = _context.Resources.GetString(Resource.String.LastModification);
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

        public override void BindItemToViewHolder(Models.Product item, ProductAdapterViewHolder viewHolder)
        {
            byte[] bytes = null;
            Bitmap bitmap = null;
            string image = null;

            if (item.Avatar == null)
            {
                image = Constants.DefaultBase64QuestionMarkIcon;
            }

            bytes = Convert.FromBase64String(Constants.DefaultBase64QuestionMarkIcon);
            bitmap = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
            viewHolder.ProductRowItemName.Text = item.Name;
            viewHolder.ProductRowItemLastModification.Text = string.Format(_lastModificationFormat, item.LastModifiedAt.ToString("U"));
            viewHolder.ProductRowItemCount.Text = string.Format(_itemsFormat, item.ProductDetails.Sum(pd => pd.Count));
            viewHolder.ProductRowItemImage.SetImageBitmap(bitmap);

            _styleProvider
                .Execute(item.LastModifiedAt,
                viewHolder.ProductRowItemName);

            viewHolder.ProductRowItemInfo.SetOnClickListener(IOnClickListener);
            viewHolder.ProductRowItemEdit.SetOnClickListener(IOnClickListener);
            viewHolder.ProductRowItemInfo.Visibility = ReadVisibility;
            viewHolder.ProductRowItemEdit.Visibility = EditVisibility;

            viewHolder.ProductRowItemInfo.Tag = viewHolder;
            viewHolder.ProductRowItemEdit.Tag = viewHolder;
        }

        public override ProductAdapterViewHolder CreateViewHolder(View view) => new ProductAdapterViewHolder(view);
    }
}