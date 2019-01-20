using Android.Graphics;
using Android.Widget;
using System;

namespace Client.Providers
{
    public class ColorConditionalStyleProvider
    {
        private static ColorConditionalStyleProvider instance = null;
        private static readonly object padlock = new object();
        private DateTime _currentDate;

        public ColorConditionalStyleProvider()
        {
            _currentDate = DateTime.UtcNow;
        }

        public void Execute(DateTime dateToCompare, params TextView[] textViews)
        {
            if(Math.Abs((dateToCompare - _currentDate).TotalDays) <= 1.0D)
            {
                foreach (var textView in textViews)
                {
                    textView.SetTextColor(Color.LightGreen);
                }
            }
        }

        public static ColorConditionalStyleProvider Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ColorConditionalStyleProvider();
                    }
                    return instance;
                }
            }
        }
    }
}