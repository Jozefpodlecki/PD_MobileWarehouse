using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Views;
using Client.Adapters;
using Client.Managers;
using Client.Services;
using Client.ViewHolders;
using Common;

namespace Client.Fragments
{
    public class Products : BaseListFragment
    {
        private ProductAdapter _adapter;

        public Products() : base(
            Resource.String.NoProductsAvailable,
            Resource.String.SearchProduct
            )
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var token = CancelAndSetTokenForView(ItemList);

            SetLoadingContent();

            _adapter = new ProductAdapter(Context, RoleManager);
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
            var result = await ProductService.GetProducts(Criteria, token);

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
            var viewHolder = (ProductAdapterViewHolder)view.Tag;

            if(view.Id == Resource.Id.ProductRowItemInfo)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToProductDetails(item);
            }
            if (view.Id == Resource.Id.ProductRowItemEdit)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToProductEdit(item);
            }
        }
    }
}