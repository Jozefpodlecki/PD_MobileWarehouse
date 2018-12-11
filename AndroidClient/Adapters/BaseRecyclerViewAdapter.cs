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
    public abstract class BaseRecyclerViewAdapter<T,H> : RecyclerView.Adapter
        where T: Java.Lang.Object, new()
        where H: RecyclerView.ViewHolder
    {
        private List<T> _items;
        private LayoutInflater _layoutInflater;
        private int _rowItemResourceId;
        public View.IOnClickListener IOnClickListener { get; set; }

        public BaseRecyclerViewAdapter(
            Context context,
            int rowItemResourceId)
        {
            _items = new List<T>();
            _layoutInflater = LayoutInflater.From(context);
            _rowItemResourceId = rowItemResourceId;
        }

        public override int ItemCount => _items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _items[position];
            var viewHolder = holder as H;

            BindItemToViewHolder(item, viewHolder);
        }

        public virtual void UpdateList(List<T> items)
        {
            _items = items;
            NotifyDataSetChanged();
        }

        public T GetItem(int position)
        {
            return _items[position];
        }

        public void RemoveItem(int position)
        {
            _items.RemoveAt(position);
            NotifyItemRemoved(position);
        }

        public abstract void BindItemToViewHolder(T item, H viewHolder);

        public abstract H CreateViewHolder(View view);

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = _layoutInflater.Inflate(_rowItemResourceId, parent, false);

            return CreateViewHolder(view);
        }


    }
}