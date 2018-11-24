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
using Client.Services;
using Client.ViewHolders;
using Common.DTO;

namespace Client.Adapters
{
    public class LocationRowItemAdapter : RecyclerView.Adapter,
        View.IOnClickListener
        
    {
        private List<Location> _items;
        private LayoutInflater _layoutInflater;
        private LocationService _service;

        public LocationRowItemAdapter(
            Context context,
            LocationService service)
        {
            _items = new List<Location>();
            _layoutInflater = LayoutInflater.From(context);
            _service = service;
        }

        public void UpdateList(List<Location> items)
        {
            _items = items;
            NotifyDataSetChanged();
        }

        public override int ItemCount => _items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _items[position];

            var viewHolder = holder as LocationRowItemViewHolder;

            viewHolder.LocationRowItemName.Text = item.Name;
            viewHolder.LocationRowItemDelete.SetOnClickListener(this);
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = _layoutInflater.Inflate(Resource.Layout.LocationsRowItem, parent, false);

            return new LocationRowItemViewHolder(view);
        }

        public async void OnClick(View view)
        {
            var holder = view.Tag as LocationRowItemViewHolder;
            var position = holder.AdapterPosition;
            var item = _items[position];

            await _service.DeleteLocation(item.Id);

            _items.RemoveAt(position);
            NotifyItemRemoved(position);
        }
    }
}