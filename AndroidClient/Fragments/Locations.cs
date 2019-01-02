﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Views;
using Client.Adapters;
using Client.Services;
using Client.ViewHolders;
using Common;

namespace Client.Fragments
{
    public class Locations : BaseListFragment
    {
        private LocationRowItemAdapter _adapter;

        public Locations() : base(
            PolicyTypes.Locations.Add,
            Resource.String.NoLocationsAvailable,
            Resource.String.TypeInILocation
            )
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var token = CancelAndSetTokenForView(ItemList);

            SetLoadingContent();

            _adapter = new LocationRowItemAdapter(Context, RoleManager);
            _adapter.IOnClickListener = this;

            ItemList.SetAdapter(_adapter);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);

            return view;
        }

        public override async Task GetItems(CancellationToken token)
        {
            var result = await LocationService.GetLocations(Criteria, token);

            RunOnUiThread(() =>
            {
                if (result.Error.Any())
                {
                    ShowToastMessage(Resource.String.ErrorOccurred);

                    return;
                }

                _adapter.UpdateList(result.Data);

                if (result.Data.Any())
                {
                    SetContent();

                    return;
                }

                SetEmptyContent();
            });
        }

        public override void OnClick(View view)
        {
            var viewHolder = view.Tag as LocationRowItemViewHolder;

            if (view.Id == Resource.Id.LocationRowItemEdit)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToEditLocation(item);
            }
            if (view.Id == Resource.Id.LocationRowItemDelete)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                _adapter.RemoveItem(item);

                Task.Run(async () =>
                {
                    await LocationService.DeleteLocation(item.Id);
                });

            }
            if (view.Id == AddItemFloatActionButton.Id)
            {
                NavigationManager.GoToAddLocation();
            }
        }
    }
}