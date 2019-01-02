using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Client.Helpers
{
    public static class Helpers
    {
        public static CancellationToken CancelAndSetTokenForView(View view)
        {
            var wrapper = view.Tag as JavaObjectWrapper<CancellationTokenSource>;

            if (wrapper != null)
            {
                if (!wrapper.Data.IsCancellationRequested)
                {
                    wrapper.Data.Cancel();
                }
            }

            wrapper = new JavaObjectWrapper<CancellationTokenSource>(new CancellationTokenSource());
            view.Tag = wrapper;

            return wrapper.Data.Token;
        }
    }
}