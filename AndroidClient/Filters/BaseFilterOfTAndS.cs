using System.Linq;
using Android.Widget;
using Client.Adapters;
using Java.Lang;

namespace Client.Filters
{
    public class BaseFilter<T, S> : Filter
        where T : new()
        where S : Services.Service
    {
        protected BaseArrayAdapter<T, S> _adapter;
        protected S _service;

        public BaseFilter(BaseArrayAdapter<T, S> adapter)
        {
            _adapter = adapter;
            _service = adapter.Service;
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