using Android.Support.V7.Widget;
using Android.Views;
using Client.ViewHolders;
using Common.DTO;
using System.Collections.Generic;

namespace Client.Adapters
{
    public class CounterpartiesRowItemAdapter : RecyclerView.Adapter
    {
        private List<Counterparty> _items;

        public CounterpartiesRowItemAdapter(List<Counterparty> items)
        {
            _items = items;
        }

        public void UpdateList(List<Counterparty> items)
        {
            _items = items;
            NotifyDataSetChanged();
        }

        public override int ItemCount => _items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as CounterpartiesRowItemViewHolder;

            var item = _items[position];

            viewHolder.CounterpartiesRowItemName.Text = item.Name;
            viewHolder.CounterpartiesRowItemNIP.Text = item.NIP;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.CounterpartiesRowItem, parent, false);

            return new CounterpartiesRowItemViewHolder(itemView);
        }
    }
}