using System.Linq;
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
            SiteClaimValues.Locations.Add,
            Resource.String.NoLocationsAvailable,
            Resource.String.TypeInILocation
            )
        {
        }

        public override void OnItemsLoad(CancellationToken token)
        {
            _adapter = new LocationRowItemAdapter(Context, RoleManager);
            _adapter.IOnClickListener = this;

            ItemList.SetAdapter(_adapter);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);
        }

        public override async Task GetItems(CancellationToken token)
        {
            var result = await LocationService.GetLocations(Criteria, token);

            if (!CheckForAuthorizationErrors(result.Error)) return;

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