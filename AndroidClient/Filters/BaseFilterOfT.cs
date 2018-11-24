using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Java.Lang;

namespace Client.Filters
{
    public class BaseFilter<T> : Filter
        where T : Java.Lang.Object, new()
    {
        private BaseArrayAdapter<T> _adapter;

        public BaseFilter(BaseArrayAdapter<T> adapter)
        {
            _adapter = adapter;
        }

        public override ICharSequence ConvertResultToStringFormatted(Java.Lang.Object resultValue)
        {
            return base.ConvertResultToStringFormatted(resultValue);
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            var filterResults = new FilterResults();
            string phrase = null;

            if (constraint == null)
            {
                return filterResults;
            }

            phrase = constraint.ToString();

            if (!_adapter.Items.Any())
            {
                return filterResults;
            }

            var filteredItems = _adapter
                .Items
                .Where(it => it.ToString().Normalize().Contains(phrase, StringComparison.InvariantCultureIgnoreCase))
                .ToArray();

            filterResults.Count = filteredItems.Count();
            filterResults.Values = filteredItems;

            return filterResults;
        }

        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            if (results.Count == 0)
            {
                _adapter.NotifyDataSetInvalidated();
                return;
            }

            _adapter.Clear();

            _adapter.AddAll((Java.Lang.Object[])results.Values);

            _adapter.NotifyDataSetChanged();
            
        }
    }
}