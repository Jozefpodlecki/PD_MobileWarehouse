using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Fragments
{
    public class Invoices : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddInvoiceFloatActionButton { get; set; }
        public AutoCompleteTextView SearchInvoice { get; set; }
        public RecyclerView InvoicesList { get; set; }
        public TextView EmptyInvoiceView { get; set; }
        public NoteService _service;
        public InvoiceRowItemAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            LayoutView = inflater.Inflate(Resource.Layout.Invoices, container, false);

            AddInvoiceFloatActionButton = LayoutView.FindViewById<FloatingActionButton>(Resource.Id.AddInvoiceFloatActionButton);
            SearchInvoice = LayoutView.FindViewById<AutoCompleteTextView>(Resource.Id.SearchInvoice);
            InvoicesList = LayoutView.FindViewById<RecyclerView>(Resource.Id.InvoicesList);
            EmptyInvoiceView = LayoutView.FindViewById<TextView>(Resource.Id.EmptyInvoiceView);

            AddInvoiceFloatActionButton.SetOnClickListener(this);
            SearchInvoice.AddTextChangedListener(this);

            LayoutManager = new LinearLayoutManager(Activity);
            LayoutManager.Orientation = LinearLayoutManager.Vertical;
            InvoicesList.SetLayoutManager(LayoutManager);

            _adapter = new InvoiceRowItemAdapter();

            InvoicesList.SetAdapter(_adapter);

            GetInvoices();

            return LayoutView;
        }
        
        public void GetInvoices()
        {
            var token = CancelAndSetTokenForView(InvoicesList);
            
            var task = Task.Run(async () =>
            {
                HttpResult<List<Common.DTO.Invoice>> result = null;
                result = await NoteService.GetInvoices(Criteria, token);

                Activity.RunOnUiThread(() =>
                {
                    if (result.Error != null)
                    {
                        EmptyInvoiceView.Visibility = ViewStates.Visible;
                        InvoicesList.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        _adapter.UpdateList(result.Data);

                        EmptyInvoiceView.Visibility = ViewStates.Invisible;
                        InvoicesList.Visibility = ViewStates.Visible;
                    }

                    _adapter.UpdateList(result.Data);
                });

            }, token);

            
        }

        public void AfterTextChanged(IEditable s)
        {
            Criteria.Name = s.ToString();

            GetInvoices();
        }

        public void OnClick(View view)
        {
            NavigationManager.GoToAddInvoice();
        }


    }
}