using Android.Views;
using Client.Adapters;
using Client.ViewHolders;
using Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments
{
    public class Counterparties : BaseListFragment
    {
        private CounterpartiesRowItemAdapter _adapter;

        public Counterparties() : base(
            SiteClaimValues.Counterparties.Add,
            Resource.String.NoCounterpartiesAvailable,
            Resource.String.SearchCounterparty)
        {
        }

        public override void OnItemsLoad(CancellationToken token)
        {
            _adapter = new CounterpartiesRowItemAdapter(Context, RoleManager);
            _adapter.IOnClickListener = this;

            ItemList.SetAdapter(_adapter);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);
        }

        public override async Task GetItems(CancellationToken token)
        {
            var result = await CounterpartyService.GetCounterparties(Criteria, token);

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
            var viewHolder = view.Tag as CounterpartiesRowItemViewHolder;

            if (view.Id == Resource.Id.CounterpartiesRowItemInfo)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToCounterpartyDetails(item);
            }
            if (view.Id == Resource.Id.CounterpartiesRowItemEdit)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToCounterpartyEdit(item);
            }
            if (view.Id == Resource.Id.CounterpartiesRowItemDelete)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                _adapter.RemoveItem(item);

                Task.Run(async () =>
                {
                    await CounterpartyService.DeleteCounterparty(item.Id);
                });
                
            }
            if (view.Id == AddItemFloatActionButton.Id)
            {
                NavigationManager.GoToAddCounterparty();
            }
        }
    }
}