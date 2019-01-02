using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Client.Filters;
using Java.Lang;

namespace Client.Adapters
{
    public class BaseArrayAdapter<T> : ArrayAdapter<T>
        where T : Java.Lang.Object, new()
    {
        protected LayoutInflater _layoutInflater { get; set; }

        protected int _textViewResourceId;
        protected int _resourceId;
        public View.IOnClickListener IOnClickListener { get; set; }
        public List<T> Items;
        private Context context;

        public BaseArrayAdapter(
            Context context,
            int resourceId = Android.Resource.Layout.SimpleListItem1,
            int textViewResourceId = Android.Resource.Id.Text1
            ) : base(context, resourceId, textViewResourceId)
        {
            _layoutInflater = LayoutInflater.From(Context);
            _resourceId = resourceId;
            _textViewResourceId = textViewResourceId;
            Items = new List<T>();

        }

        public BaseArrayAdapter(
            Context context,
            int resourceId
            ) : base(context, resourceId)
        {
            _layoutInflater = LayoutInflater.From(Context);
            _resourceId = resourceId;
            Items = new List<T>();

        }

        public new void Add(Object item)
        {
            base.Add(item);
            Items.Add((T)item);
        }

        public new void Remove(Object item)
        {
            base.Remove(item);
            Items.Remove((T)item);
        }

        public void UpdateList(List<T> items)
        {
            base.Clear();
            Items.Clear();
            Items = items;
            base.AddAll(items);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if(convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
            }

            var view = convertView.FindViewById<TextView>(_textViewResourceId);

            var item = GetItem(position);

            view.Text = item.ToString();
            view.Tag = item;

            return convertView;
        }

        public override Filter Filter => new BaseFilter<T>(this);
    }
}