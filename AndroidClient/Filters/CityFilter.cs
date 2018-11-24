using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Models;
using Client.Services;
using Common;
using Java.Lang;

namespace Client.Filters
{
    public class CityFilter : BaseFilter<Models.City, Services.NoteService>
    {
        private FilterCriteria _criteria;
        private CancellationTokenSource _cancellationTokenSource;

        public CityFilter(BaseArrayAdapter<City, NoteService> adapter) : base(adapter)
        {
            _criteria = new FilterCriteria
            {
                ItemsPerPage = 10
            };
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            if(_cancellationTokenSource == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();

            }
            else
            {
                if (!_cancellationTokenSource.IsCancellationRequested)
                {
                    _cancellationTokenSource.Cancel();
                }

                _cancellationTokenSource = new CancellationTokenSource();
            }

            var filterResults = new FilterResults();
            
            if (constraint == null)
            {
                return filterResults;
            }

            Task.Run(async () =>
            {
                _criteria.Name = constraint.ToString();
                var result = await _service.GetCities(_criteria, _cancellationTokenSource.Token);

                filterResults.Values = result.Data.ToArray();
                filterResults.Count = result.Data.Count;
                PublishResults(constraint, filterResults);

            }, _cancellationTokenSource.Token);

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