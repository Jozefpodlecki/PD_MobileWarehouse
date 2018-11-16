
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common;
using Java.Lang;

namespace Client.Fragments
{
    public class Invoices : Fragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddInvoiceFloatActionButton { get; set; }
        public AutoCompleteTextView SearchInvoice { get; set; }
        public RecyclerView InvoicesList { get; set; }
        public TextView EmptyInvoiceView { get; set; }
        public FilterCriteria Criteria { get; set; }
        public NoteService _service;
        public InvoiceRowItemAdapter _adapter;
        public new MainActivity Activity => (MainActivity)base.Activity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Invoices, container, false);

            AddInvoiceFloatActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddInvoiceFloatActionButton);
            SearchInvoice = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchInvoice);
            InvoicesList = view.FindViewById<RecyclerView>(Resource.Id.InvoicesList);
            EmptyInvoiceView = view.FindViewById<TextView>(Resource.Id.EmptyInvoiceView);

            AddInvoiceFloatActionButton.SetOnClickListener(this);

            return view;
        }

        public void GetInvoices()
        {

        }

        public void AfterTextChanged(IEditable s)
        {
            Criteria.Name = s.ToString();

            GetInvoices();
        }

        public void OnClick(View view)
        {
            Activity.NavigationManager.GoToAddInvoice();
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after) { }
        public void OnTextChanged(ICharSequence s, int start, int before, int count) { }
    }
}