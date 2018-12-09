using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Client.Filters;

namespace Client.Adapters
{
    public class BaseArrayAdapter<T> : ArrayAdapter<T>
        where T : Java.Lang.Object, new()
    {
        protected LayoutInflater _layoutInflater { get; set; }

        protected int _textViewResourceId;
        protected int _resourceId;
        public List<T> Items;

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
            if(convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
            }

            var view = convertView.FindViewById<TextView>(_textViewResourceId);

            view.Text = GetItem(position).ToString();
            
            return convertView;
        }

        public override Filter Filter => new BaseFilter<T>(this);
    }
}