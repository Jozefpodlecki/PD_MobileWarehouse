
using Android.App;
using Android.Icu.Util;
using Android.OS;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Helpers;
using Client.Services;
using Common;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Android.App.DatePickerDialog;

namespace Client.Fragments.Add
{
    public class Invoice : BaseFragment,
        View.IOnClickListener,
        IOnDateSetListener,
        View.IOnFocusChangeListener
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
        public ImageButton AddInvoiceAddProductButton { get; set; }
        private DatePickerDialog _dialog;
        private AddInvoiceEntryRowItemAdapter _productsAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var LayoutView = inflater.Inflate(Resource.Layout.AddInvoice, container, false);

            var token = CancelAndSetTokenForView(LayoutView);

            //var actionBar = Activity.SupportActionBar;
            //actionBar.Title = "Add Invoice";

            AddInvoiceType = LayoutView.FindViewById<Spinner>(Resource.Id.AddInvoiceType);
            AddInvoiceCounterparty = LayoutView.FindViewById<AutoCompleteTextView>(Resource.Id.AddInvoiceCounterparty);
            AddInvoiceDocumentId = LayoutView.FindViewById<EditText>(Resource.Id.AddInvoiceDocumentId);
            AddInvoiceIssueDate = LayoutView.FindViewById<EditText>(Resource.Id.AddInvoiceIssueDate);
            AddInvoiceCompletionDate = LayoutView.FindViewById<EditText>(Resource.Id.AddInvoiceCompletionDate);
            AddInvoiceCity = LayoutView.FindViewById<AutoCompleteTextView>(Resource.Id.AddInvoiceCity);
            AddInvoicePaymentMethod = LayoutView.FindViewById<Spinner>(Resource.Id.AddInvoicePaymentMethod);
            AddInvoiceProducts = LayoutView.FindViewById<ListView>(Resource.Id.AddInvoiceProducts);
            AddInvoiceButton = LayoutView.FindViewById<Button>(Resource.Id.AddInvoiceButton);
            AddInvoiceAddProductButton = LayoutView.FindViewById<ImageButton>(Resource.Id.AddInvoiceAddProductButton);

            _dialog = CreateDatePickerDialog(this);
            
            AddInvoiceIssueDate.OnFocusChangeListener = this;
            AddInvoiceCompletionDate.OnFocusChangeListener = this;
            AddInvoiceAddProductButton.SetOnClickListener(this);
            AddInvoiceButton.SetOnClickListener(this);
            
            var invoiceTypeAdapter = new SpinnerDefaultValueAdapter<Models.KeyValue>(Activity);
            var paymentMethodAdapter = new SpinnerDefaultValueAdapter<Models.KeyValue>(Activity);
            var counterPartyAdapter = new BaseArrayAdapter<Models.Counterparty>(Activity);
            _productsAdapter = new AddInvoiceEntryRowItemAdapter(Activity);

            AddInvoiceProducts.Adapter = _productsAdapter;

            var products = new List<Models.Entry>()
            {
                new Models.Entry()
            };

            _productsAdapter.UpdateList(products);

            AddInvoiceType.Adapter = invoiceTypeAdapter;
            AddInvoicePaymentMethod.Adapter = paymentMethodAdapter;
            AddInvoiceCounterparty.Adapter = counterPartyAdapter;

            var task = Task.Run(async () =>
            {
                try
                {
                    var counterpartyResult = await NoteService.GetCounterparties(Criteria, token);
                    var paymentMethodsResult = await NoteService.GetPaymentMethods(token);
                    var invoiceTypesResult = await NoteService.GetInvoiceTypes(token);

                    paymentMethodsResult.Data.Insert(0, new Models.KeyValue { Id = -1, Name = "Select payment method" });
                    invoiceTypesResult.Data.Insert(0, new Models.KeyValue { Id = -1, Name = "Select invoice type" });

                    Activity.RunOnUiThread(() =>
                    {
                        invoiceTypeAdapter.UpdateList(paymentMethodsResult.Data);
                        paymentMethodAdapter.UpdateList(invoiceTypesResult.Data);
                        counterPartyAdapter.UpdateList(counterpartyResult.Data);
                    });

                }
                catch (Exception ex)
                {

                }
               

            }, token);
            
            return LayoutView;
        }

        public void OnClick(View view)
        {
            if(view == AddInvoiceButton)
            {
                AddInvoice();   
            }
            if(view == AddInvoiceAddProductButton)
            {
                _productsAdapter.Add(new Models.Entry());
                _productsAdapter.NotifyDataSetChanged();
            }
        }

        public bool Validate()
        {
            if(AddInvoiceType.SelectedItem == null)
            {
                return false;
            }
            if (AddInvoicePaymentMethod.SelectedItem == null)
            {
                return false;
            }

            if (!(AddInvoiceIssueDate.Tag is JavaObjectWrapper<DateTime>))
            {
                return false;
            }

            if (!(AddInvoiceCompletionDate.Tag is JavaObjectWrapper<DateTime>))
            {
                return false;
            }

            return true;
        }

        public async void AddInvoice()
        {
            var token = CancelAndSetTokenForView(AddInvoiceButton);

            //Validate();
            var issueDate = AddInvoiceIssueDate.Tag as JavaObjectWrapper<DateTime>;
            var completionDate = AddInvoiceCompletionDate.Tag as JavaObjectWrapper<DateTime>;
            var invoiceType = AddInvoiceType.SelectedItem as Models.KeyValue;
            var paymentMethod = AddInvoicePaymentMethod.SelectedItem as Models.KeyValue;

            var items = _productsAdapter.Items;

            var invoice = new Common.DTO.Invoice
            {
                DocumentId = AddInvoiceDocumentId.Text,
                IssueDate = issueDate.Data,
                CompletionDate = completionDate.Data,
                InvoiceType = (InvoiceType)invoiceType.Id,
                PaymentMethod = (PaymentMethod)paymentMethod.Id
            };

            await NoteService.AddInvoice(invoice);

            NavigationManager.GoToInvoices();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            var targetView = view.Tag as EditText;

            var datetime = new DateTime(year, month, dayOfMonth, Calendar);
            targetView.Tag = new JavaObjectWrapper<DateTime>(datetime);
            targetView.Text = datetime.ToLongDateString();
            targetView.ClearFocus();
        }

        public void OnFocusChange(View view, bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }

            if (view == AddInvoiceCompletionDate)
            {
                _dialog.DatePicker.Tag = AddInvoiceCompletionDate;
                _dialog.Show();
            }
            if (view == AddInvoiceIssueDate)
            {
                _dialog.DatePicker.Tag = AddInvoiceIssueDate;
                _dialog.Show();
            }
        }
    }
}