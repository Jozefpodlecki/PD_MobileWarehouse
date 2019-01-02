using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace Client.Adapters
{
    public class SpinnerDefaultValueAdapter<T> : BaseArrayAdapter<T>
        where T : Java.Lang.Object, new()
    {
        public SpinnerDefaultValueAdapter(
            Context context,
            int resourceId = Android.Resource.Layout.SimpleSpinnerItem,
            int textViewResourceId = Android.Resource.Id.Text1) : base(context, resourceId, textViewResourceId)
        {
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetDropDownView(position, convertView, parent);

            var textView = (TextView)view;
            var color = Color.Black;

            if (position == 0)
            {
                color = Color.Gray;
            }

            textView.SetTextColor(color);

            return view;
        }

        public override bool IsEnabled(int position) => position != 0;

    }
}