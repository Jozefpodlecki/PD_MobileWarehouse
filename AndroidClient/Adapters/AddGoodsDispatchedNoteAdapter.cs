using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Services;
using Client.Services.Interfaces;
using Client.ViewHolders;
using Common;

namespace Client.Adapters
{
    public class AddGoodsDispatchedNoteAdapter : BaseArrayAdapter<Models.NoteEntry>
    {
        public FilterCriteria Criteria { get; set; }
        private ILocationService _locationService;
        private BaseArrayAdapter<Models.Location> _locationAdapter;
        private Activity _activity;

        public AddGoodsDispatchedNoteAdapter(
            Context context,
            ILocationService locationService,
            int resourceId = Resource.Layout.AddGoodsDispatchedNoteRowItem) : base(context, resourceId)
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
            AddGoodsDispatchedNoteAdapterViewHolder viewHolder = null;

            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
                viewHolder = new AddGoodsDispatchedNoteAdapterViewHolder(convertView);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (AddGoodsDispatchedNoteAdapterViewHolder)convertView.Tag;
            }

            var item = GetItem(position);

            viewHolder.Position = position;
            viewHolder.AddGoodsDispatchedNoteRowItemProductName.Text = item.Name;
            viewHolder.AddGoodsDispatchedNoteRowItemLocation.Adapter = _locationAdapter;

            var token = Helpers.Helpers.CancelAndSetTokenForView(viewHolder.AddGoodsDispatchedNoteRowItemLocation);

            Task.Run(async () =>
            {
                var adapter = new BaseArrayAdapter<Models.Location>(Context, Android.Resource.Layout.SimpleListItem1);

                var result = await _locationService.GetLocationsByProduct("name", item.Name, token);

                _activity.RunOnUiThread(() =>
                {
                    viewHolder.AddGoodsDispatchedNoteRowItemLocation.Adapter = adapter;
                    adapter.UpdateList(result.Data);
                });
                
            }, token);

            return convertView;
        }

    }
}