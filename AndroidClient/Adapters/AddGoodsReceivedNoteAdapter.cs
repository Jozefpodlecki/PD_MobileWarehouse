using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Models;
using Client.Services;
using Client.ViewHolders;
using Common;

namespace Client.Adapters
{
    public class AddGoodsReceivedNoteAdapter : BaseArrayAdapter<Models.NoteEntry>
    {
        public FilterCriteria Criteria { get; set; }
        private LocationService _locationService;
        private BaseArrayAdapter<Location> _locationAdapter;
        private Activity _activity;
        private CancellationTokenSource _cancellationTokenSource;

        public AddGoodsReceivedNoteAdapter(
            Context context,
            LocationService locationService,
            int resourceId = Resource.Layout.AddGoodsReceivedNoteRowItem
            ) : base(context, resourceId)
        {
            Criteria = new FilterCriteria()
            {
                ItemsPerPage = 10
            };
            _locationService = locationService;
            _locationAdapter = new BaseArrayAdapter<Models.Location>(Context);
            _activity = (Activity)Context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            AddGoodsReceivedNoteAdapterViewHolder viewHolder = null;

            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
                viewHolder = new AddGoodsReceivedNoteAdapterViewHolder(convertView);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (AddGoodsReceivedNoteAdapterViewHolder)convertView.Tag;
            }

            var item = GetItem(position);

            viewHolder.Position = position;
            viewHolder.AddGoodsReceivedNoteRowItemProductName.Text = item.Name;
            viewHolder.AddGoodsReceivedNoteRowItemLocation.Adapter = _locationAdapter;
            viewHolder.AddGoodsReceivedNoteRowItemLocation.Threshold = 1;
            viewHolder.AddGoodsReceivedNoteRowItemLocation.AfterTextChanged += AfterTextChanged;
            viewHolder.AddGoodsReceivedNoteRowItemLocation.ItemClick += OnAutocompleteLocationClick;
            
            viewHolder.AddGoodsReceivedNoteRowItemLocation.Tag = viewHolder;

            return convertView;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {

            if(_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            Criteria.Name = eventArgs.Editable.ToString();

            Task.Run(async () =>
            {
                var result = await _locationService.GetLocations(Criteria, token);

                _activity.RunOnUiThread(() =>
                {
                    _locationAdapter.UpdateList(result.Data);
                });
            }, token);
        }

        private void OnAutocompleteLocationClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var item = (AddGoodsReceivedNoteAdapterViewHolder)editText.Tag;
            var locationItem = _locationAdapter.GetItem(eventArgs.Position);

            var noteEntry = GetItem(item.Position);
            noteEntry.Location = locationItem;
        }
    }
}