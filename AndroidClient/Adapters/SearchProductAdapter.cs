using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Services;
using Java.Interop;
using Java.Lang;

namespace Client.Adapters
{
    public class SearchProductFilter : Filter
    {
        private readonly ArrayAdapter<string> _adapter;
        private readonly Activity _activity;
        private readonly ProductService _productService;

        public SearchProductFilter(
            Activity activity,
            ArrayAdapter<string> adapter)
        {
            _activity = activity;
            _adapter = adapter;
            _productService = new ProductService(_activity);
        }

        public override ICharSequence ConvertResultToStringFormatted(Java.Lang.Object resultValue)
        {
            return base.ConvertResultToStringFormatted(resultValue);
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            var filterResults = new FilterResults();

            if(constraint == null)
            {
                return filterResults;
            }

            var result = _productService.GetProductNames(constraint.ToString()).Result;

            filterResults.Values = result.ToArray();
            filterResults.Count = result.Count;

            return filterResults;
        }

        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            if(results != null && results.Count > 0) {
                _adapter.NotifyDataSetChanged();
            }
            else
            {
                _adapter.NotifyDataSetInvalidated();
            }
        }
    }

    public class SearchProductAdapter : ArrayAdapter<string>, IFilterable
    {
        private readonly Activity _activity;

        public SearchProductAdapter(Activity activity, int textViewResourceId, string[] objects) : base(activity, textViewResourceId, objects)
        {
            _activity = activity;
        }

        public override int Count => base.Count;

        public override Filter Filter => new SearchProductFilter(_activity, this);

        public override long GetItemId(int position)
        {
            return base.GetItemId(position);
        }
    }
}