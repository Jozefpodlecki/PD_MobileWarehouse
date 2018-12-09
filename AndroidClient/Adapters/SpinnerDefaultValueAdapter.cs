using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace Client.Adapters
{
    public class SpinnerDefaultValueAdapter<T> : BaseArrayAdapter<T>
        where T : Java.Lang.Object, new()
    {
        public SpinnerDefaultValueAdapter(Context context, int resourceId = 17367043, int textViewResourceId = 16908308) : base(context, resourceId, textViewResourceId)
        {
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetDropDownView(position, convertView, parent);

            var textView = (TextView)view;

            if (position == 0)
            {
                textView.SetTextColor(Color.Gray);
            }
            else
            {
                textView.SetTextColor(Color.Black);
            }

            return view;
        }

        public override bool IsEnabled(int position)
        {
            if (position == 0)
            {
                return false;
            }

            return true;
        }
        
    }
}