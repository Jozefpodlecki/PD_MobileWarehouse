﻿using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Client.ViewHolders;
using Common.DTO;
using System;
using System.Collections.Generic;

namespace Client.Adapters
{
    public class ProductAdapter : RecyclerView.Adapter
    {
        private List<BasicProduct> _products;
        private Activity _activity;

        public ProductAdapter(
            Activity activity
            )
        {
            _activity = activity;
            _products = new List<BasicProduct>();
        }

        public void UpdateList(List<BasicProduct> products)
        {
            _products = products;
            NotifyDataSetChanged();
        }

        public override int ItemCount => _products.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var productAdapterViewHolder = holder as ProductAdapterViewHolder;

            var product = _products[position];

            byte[] bytes = null;
            Bitmap bitmap = null;
            string image = null;

            if(product.Image == null)
            {
                image = defaultIcon;
            }

            bytes = Convert.FromBase64String(defaultIcon);
            bitmap = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
            productAdapterViewHolder.Product.Text = product.Name;
            productAdapterViewHolder.LastModification.Text = product.LastModification.ToShortDateString();
            productAdapterViewHolder.Image.SetImageBitmap(bitmap);

        }

        public static string defaultIcon = "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";

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.ProductRowItem, parent, false);

            return new ProductAdapterViewHolder(itemView);
        }
    }
}