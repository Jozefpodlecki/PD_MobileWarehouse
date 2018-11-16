
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Add
{
    public class Invoice : Fragment,
        View.IOnClickListener
    {
        public Spinner AddInvoiceType { get; set; }
        public AutoCompleteTextView AddInvoiceCounterparty { get; set; }
        public EditText AddInvoiceDocumentId { get; set; }
        public EditText AddInvoiceIssueDate { get; set; }
        public EditText AddInvoiceCompletionDate { get; set; }
        public AutoCompleteTextView AddInvoiceCity { get; set; }
        public Spinner AddInvoicePaymentMethod { get; set; }
        public ListView AddInvoiceProducts { get; set; }
        public Button AddInvoiceButton { get; set; }
        public new MainActivity Activity => (MainActivity)base.Activity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Invoices, container, false);

            AddInvoiceType = view.FindViewById<Spinner>(Resource.Id.AddInvoiceType);
            AddInvoiceCounterparty = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddInvoiceCounterparty);
            AddInvoiceDocumentId = view.FindViewById<EditText>(Resource.Id.AddInvoiceDocumentId);
            AddInvoiceIssueDate = view.FindViewById<EditText>(Resource.Id.AddInvoiceIssueDate);
            AddInvoiceCompletionDate = view.FindViewById<EditText>(Resource.Id.AddInvoiceCompletionDate);
            AddInvoiceCity = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddInvoiceCity);
            AddInvoicePaymentMethod = view.FindViewById<Spinner>(Resource.Id.AddInvoicePaymentMethod);
            AddInvoiceProducts = view.FindViewById<ListView>(Resource.Id.AddInvoiceProducts);
            AddInvoiceButton = view.FindViewById<Button>(Resource.Id.AddInvoiceButton);

            AddInvoiceButton.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View v)
        {
            Activity.NavigationManager.GoToInvoices();
        }
    }
}