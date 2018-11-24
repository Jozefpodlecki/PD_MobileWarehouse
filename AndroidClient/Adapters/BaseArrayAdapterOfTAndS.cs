using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Client.Filters;

namespace Client.Adapters
{
    public class BaseArrayAdapter<T, S> : ArrayAdapter<T>
        where T : new()
        where S : Services.Service
    {
        public LayoutInflater _layoutInflater { get; set; }

        private int _textViewResourceId;
        private int _resourceId;
        public S Service;
        public List<T> Items;

        public BaseArrayAdapter(
            Context context,
            S service,
            int resourceId = Android.Resource.Layout.SimpleListItem1,
            int textViewResourceId = Android.Resource.Id.Text1
            ) : base(context, resourceId, textViewResourceId)
        {
            _layoutInflater = LayoutInflater.From(Context);
            _resourceId = resourceId;
            _textViewResourceId = textViewResourceId;
            Items = new List<T>();
            Service = service;
        }

        public void UpdateList(List<T> items)
        {
            Clear();
            Items.Clear();
            Items.AddRange(items);
            AddAll(items);
            NotifyDataSetChanged();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
            }

            var view = convertView.FindViewById<TextView>(_textViewResourceId);

            view.Text = GetItem(position).ToString();

            return convertView;
        }
    }
}