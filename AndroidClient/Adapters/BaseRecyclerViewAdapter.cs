using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.Adapters
{
    public abstract class BaseRecyclerViewAdapter<T, S, V> : RecyclerView.Adapter,
        View.IOnClickListener
        where S : Client.Services.Service
        where V : RecyclerView.ViewHolder
    {
        protected List<T> _items;
        protected LayoutInflater _layoutInflater;
        protected S _service;
        private int _resourceId;

        public BaseRecyclerViewAdapter(
            Context context,
            S service,
            int resourceId)
        {
            _items = new List<T>();
            _layoutInflater = LayoutInflater.From(context);
            _service = service;
            _resourceId = resourceId;
        }

        public void UpdateList(List<T> items)
        {
            _items = items;
            NotifyDataSetChanged();
        }

        public override int ItemCount => _items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _items[position];

            var viewHolder = holder as V;

            BindItemToViewHolder(item,viewHolder);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = _layoutInflater.Inflate(_resourceId, parent, false);

            return GetViewHolder(view);
        }

        public abstract RecyclerView.ViewHolder GetViewHolder(View view);

        public abstract void OnClick(View view);

        public abstract void BindItemToViewHolder(T dto,V holder);
    }
}