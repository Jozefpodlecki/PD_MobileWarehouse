using Android.Content;
using Android.Graphics;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Providers;
using Client.ViewHolders;
using System;

namespace Client.Adapters
{
    public class ProductAdapter : BaseRecyclerViewAdapter<Models.Product, ProductAdapterViewHolder>
    {
        public ViewStates ReadVisibility;
        public ViewStates EditVisibility;
        public ColorConditionalStyleProvider _styleProvider;

        public ProductAdapter(Context context, IRoleManager roleManager) 
            : base(context, roleManager, Resource.Layout.ProductRowItem)
        {
            ReadVisibility = roleManager.Permissions.ContainsKey(Resource.Id.ProductRowItemInfo) ? ViewStates.Visible : ViewStates.Invisible;
            EditVisibility = roleManager.Permissions.ContainsKey(Resource.Id.ProductRowItemEdit) ? ViewStates.Visible : ViewStates.Invisible;
            _styleProvider = ColorConditionalStyleProvider.Instance;
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