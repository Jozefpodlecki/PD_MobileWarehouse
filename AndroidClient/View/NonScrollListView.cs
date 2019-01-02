using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Client.Views
{
    public class NonScrollListView : ListView
    {

        public NonScrollListView(Context context) : base(context)
        {

        }

        public NonScrollListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }

        public NonScrollListView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var size = int.MaxValue >> 2;
            var customHeightMeasure = MeasureSpec.MakeMeasureSpec(size, MeasureSpecMode.AtMost);

            base.OnMeasure(widthMeasureSpec, customHeightMeasure);

            var lparams = LayoutParameters;
            lparams.Height = MeasuredHeight;
        }
    }
}