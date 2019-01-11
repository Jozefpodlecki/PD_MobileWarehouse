using Android.OS;
using Android.Text;
using Android.Views;
using Client.Adapters;
using Client.Services;
using Client.ViewHolders;
using Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments
{
    public class Invoices : BaseListFragment
    {
        private InvoiceRowItemAdapter _adapter;
        public InvoiceFilterCriteria InvoiceFilterCriteria { get; set; }

        public Invoices() : base(
            PolicyTypes.Invoices.Add,
            Resource.String.NoInvoicesAvailable,
            Resource.String.TypeInInvoice
            )
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var token = CancelAndSetTokenForView(ItemList);

            SetLoadingContent();

            InvoiceFilterCriteria = new InvoiceFilterCriteria
            {
                ItemsPerPage = 10
            };

            _adapter = new InvoiceRowItemAdapter(Context, RoleManager);
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
            var result = await InvoiceService.GetInvoices(InvoiceFilterCriteria, token);

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

        public override void AfterTextChanged(IEditable text)
        {
            SetLoadingContent();

            InvoiceFilterCriteria.Name = text.ToString();

            var token = CancelAndSetTokenForView(ItemList);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);
        }

        public override void OnClick(View view)
        {
            var viewHolder = view.Tag as InvoiceRowItemViewHolder;

            if (view.Id == Resource.Id.InvoiceRowItemDelete)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);

                Task.Run(async () =>
                {
                    var result = await InvoiceService.DeleteInvoice(item.Id);

                    if (result.Error.Any())
                    {
                        RunOnUiThread(() =>
                        {
                            ShowToastMessage(Resource.String.ErrorOccurred);
                        });

                        return;
                    }

                    RunOnUiThread(() =>
                    {
                        _adapter.RemoveItem(item);
                    });
                });
            }
            if (view.Id == Resource.Id.InvoiceRowItemInfo)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.InvoiceDetails(item);
            }
            if (view.Id == AddItemFloatActionButton.Id)
            {
                NavigationManager.GoToAddInvoice();
            }
        }
    }
}