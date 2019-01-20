using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Helpers;
using Client.Models;
using Client.Services;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Android.App.DatePickerDialog;

namespace Client.Fragments.Add
{
    public class Invoice : BaseAddFragment<Models.Invoice>,
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
        public ImageButton AddInvoiceAddProductButton { get; set; }
        private DatePickerDialog _dialog;
        private AddInvoiceEntryRowItemAdapter _productsAdapter;
        private SpinnerDefaultValueAdapter<Models.KeyValue> _invoiceTypeAdapter;
        private SpinnerDefaultValueAdapter<Models.KeyValue> _paymentMethodAdapter;
        private BaseArrayAdapter<Models.Counterparty> _counterPartyAdapter;
        private BaseArrayAdapter<City> _cityAdapter;

        public Invoice() : base(Resource.Layout.AddInvoice)
        {
            Entity = new Models.Invoice();
            Entity.City = new City();
            Entity.Counterparty = new Models.Counterparty();
            Entity.Products = new List<Entry>()
            {
                new Entry()
            };
        }

        public override void OnBindElements(View view)
        {
            AddInvoiceType = view.FindViewById<Spinner>(Resource.Id.AddInvoiceType);
            AddInvoiceCounterparty = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddInvoiceCounterparty);
            AddInvoiceDocumentId = view.FindViewById<EditText>(Resource.Id.AddInvoiceDocumentId);
            AddInvoiceIssueDate = view.FindViewById<EditText>(Resource.Id.AddInvoiceIssueDate);
            AddInvoiceCompletionDate = view.FindViewById<EditText>(Resource.Id.AddInvoiceCompletionDate);
            AddInvoiceCity = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddInvoiceCity);
            AddInvoicePaymentMethod = view.FindViewById<Spinner>(Resource.Id.AddInvoicePaymentMethod);
            AddInvoiceProducts = view.FindViewById<ListView>(Resource.Id.AddInvoiceProducts);
            AddInvoiceAddProductButton = view.FindViewById<ImageButton>(Resource.Id.AddInvoiceAddProductButton);

            _dialog = CreateDatePickerDialog(this);

            AddInvoiceIssueDate.OnFocusChangeListener = this;
            AddInvoiceCompletionDate.OnFocusChangeListener = this;
            AddInvoiceAddProductButton.SetOnClickListener(this);

            _invoiceTypeAdapter = new SpinnerDefaultValueAdapter<Models.KeyValue>(Context);
            _paymentMethodAdapter = new SpinnerDefaultValueAdapter<Models.KeyValue>(Context);
            _counterPartyAdapter = new BaseArrayAdapter<Models.Counterparty>(Context);
            _cityAdapter = new BaseArrayAdapter<City>(Context);
            _productsAdapter = new AddInvoiceEntryRowItemAdapter(Context);

            AddInvoiceProducts.Adapter = _productsAdapter;
            _productsAdapter.IOnClickListener = this;
            _productsAdapter.UpdateList(Entity.Products);

            AddInvoiceType.Adapter = _invoiceTypeAdapter;
            AddInvoicePaymentMethod.Adapter = _paymentMethodAdapter;
            AddInvoiceCounterparty.Adapter = _counterPartyAdapter;
            AddInvoiceCounterparty.ItemClick += OnAutocompleteCounterpartyClick;
            AddInvoiceCity.Adapter = _cityAdapter;
            AddInvoiceCity.ItemClick += OnAutocompleteCityClick;

            AddInvoiceCity.AfterTextChanged += AfterTextChanged;
            AddInvoiceCounterparty.AfterTextChanged += AfterTextChanged;

            var currentDate = DateTime.Now;
            var currentDateFormat = currentDate.ToLongDateString();

            AddInvoiceIssueDate.Text = currentDateFormat;
            AddInvoiceIssueDate.Tag = new JavaObjectWrapper<DateTime>(currentDate);
            AddInvoiceCompletionDate.Text = currentDateFormat;
            AddInvoiceCompletionDate.Tag = new JavaObjectWrapper<DateTime>(currentDate);
            AddInvoiceDocumentId.Text = string.Format("FAK/{0:yyyyMMddhhmmss}", currentDate);

            var token = CancelAndSetTokenForView(AddButton);

            Task.Run(async () =>
            {
                await Load(token);
            }, token);
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            if (editText.Id == AddInvoiceCity.Id)
            {
                var token = CancelAndSetTokenForView(AddInvoiceCity);

                Criteria.Name = text;

                var task = Task.Run(async () =>
                {
                    var cityResult = await CityService.GetCities(Criteria, token);

                    RunOnUiThread(() =>
                    {
                        _cityAdapter.UpdateList(cityResult.Data);
                    });
                    
                }, token);
            }
            if (editText.Id == AddInvoiceCounterparty.Id)
            {
                var token = CancelAndSetTokenForView(AddInvoiceCounterparty);

                Criteria.Name = text;

                var task = Task.Run(async () =>
                {
                    var counterpartyResult = await CounterpartyService.GetCounterparties(Criteria, token);

                    RunOnUiThread(() =>
                    {
                        _counterPartyAdapter.UpdateList(counterpartyResult.Data);
                    });
                    
                }, token);
            }
        }

        private void OnAutocompleteCounterpartyClick(object adapter, AdapterView.ItemClickEventArgs eventArgs)
        {
            var item = _counterPartyAdapter.GetItem(eventArgs.Position);

            Entity.Counterparty = item;
        }

        private void OnAutocompleteCityClick(object adapter, AdapterView.ItemClickEventArgs eventArgs)
        {
            var item = _cityAdapter.GetItem(eventArgs.Position);
            Entity.City.Id = item.Id;
            Entity.City.Name = item.Name;
        }

        public async Task Load(CancellationToken token)
        {
            var counterpartyResult = await CounterpartyService.GetCounterparties(Criteria, token);
            var paymentMethodsResult = await InvoiceService.GetPaymentMethods(token);
            var invoiceTypesResult = await InvoiceService.GetInvoiceTypes(token);

            if (counterpartyResult.Error.Any())
            {
                ShowToastMessage(Resource.String.ErrorOccurred);
                return;
            }

            var paymentMethodPrompt = Resources.GetString(Resource.String.PaymentMethodPrompt);
            var invoiceTypePrompt = Resources.GetString(Resource.String.InvoiceTypePrompt);

            paymentMethodsResult.Data[0].Name = GetString("PaymentMethod.Cash");
            paymentMethodsResult.Data[1].Name = GetString("PaymentMethod.Card");

            invoiceTypesResult.Data[0].Name = GetString("InvoiceType.Sales");
            invoiceTypesResult.Data[1].Name = GetString("InvoiceType.Purchase");

            paymentMethodsResult.Data.Insert(0, new Models.KeyValue { Id = -1, Name = paymentMethodPrompt });
            invoiceTypesResult.Data.Insert(0, new Models.KeyValue { Id = -1, Name = invoiceTypePrompt });

            RunOnUiThread(() =>
            {
                _invoiceTypeAdapter.UpdateList(paymentMethodsResult.Data);
                _paymentMethodAdapter.UpdateList(invoiceTypesResult.Data);
                _counterPartyAdapter.UpdateList(counterpartyResult.Data);
            });
        }

        public override void OnOtherButtonClick(View view)
        {
            if (view.Id == AddInvoiceAddProductButton.Id)
            {
                _productsAdapter.Add(new Models.Entry());
                _productsAdapter.NotifyDataSetChanged();
            }

            var item = view.Tag as Models.Entry;

            switch (view.Id)
            {
                case Resource.Id.AddInvoiceProductRemove:
                    if (_productsAdapter.Items.Count == 1)
                    {
                        var message = Resources.GetString(Resource.String.InvoiceEntryMinimum);
                        ShowToastMessage(message);
                        return;
                    }
                    _productsAdapter.Remove(item);
                    _productsAdapter.NotifyDataSetChanged();
                    break;
                case Resource.Id.AddInvoiceProductBarcode:
                    break;
                case Resource.Id.AddInvoiceProductQRCode:
                    break;
            }
        }

        public override bool Validate()
        {
            if (!string.IsNullOrEmpty(AddInvoiceDocumentId.Text))
            {
                ValidateRequired(AddInvoiceDocumentId);
                return false;
            }

            if (AddInvoiceType.SelectedItem == null)
            {
                ShowToastMessage(Resource.String.FillAllFields);
                return false;
            }
            if (AddInvoicePaymentMethod.SelectedItem == null)
            {
                ShowToastMessage(Resource.String.FillAllFields);
                return false;
            }

            if (!(AddInvoiceIssueDate.Tag is JavaObjectWrapper<DateTime>))
            {
                ShowToastMessage(Resource.String.FillAllFields);
                return false;
            }

            if (!(AddInvoiceCompletionDate.Tag is JavaObjectWrapper<DateTime>))
            {
                ShowToastMessage(Resource.String.FillAllFields);
                return false;
            }

            return true;
        }

        public async Task AddInvoice(CancellationToken token)
        {
            var issueDate = AddInvoiceIssueDate.Tag as JavaObjectWrapper<DateTime>;
            var completionDate = AddInvoiceCompletionDate.Tag as JavaObjectWrapper<DateTime>;
            var invoiceType = AddInvoiceType.SelectedItem as Models.KeyValue;
            var paymentMethod = AddInvoicePaymentMethod.SelectedItem as Models.KeyValue;

            Entity.DocumentId = AddInvoiceDocumentId.Text;
            Entity.IssueDate = issueDate.Data;
            Entity.CompletionDate = completionDate.Data;
            Entity.InvoiceType = (InvoiceType)invoiceType.Id;
            Entity.PaymentMethod = (PaymentMethod)paymentMethod.Id;
            Entity.Products = _productsAdapter.Items;

            if((int)Entity.InvoiceType == 255)
            {
                ShowToastMessage(Resource.String.InvalidInvoiceType);
                return;
            }

            if ((int)Entity.PaymentMethod == 255)
            {
                ShowToastMessage(Resource.String.InvalidPaymentMethod);
                return;
            }

            if (Entity
                .Products
                .Any(pr => pr.Price == 0 || pr.Count == 0 || (pr.VAT >= 0.99M && pr.VAT < 0.00M )))
            {
                ShowToastMessage(Resource.String.InvalidEntries);
                return;
            }

            var result = await InvoiceService.AddInvoice(Entity, token);

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
                NavigationManager.GoToInvoices();
            });
                
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

            if (view.Id == AddInvoiceCompletionDate.Id)
            {
                _dialog.DatePicker.Tag = AddInvoiceCompletionDate;
                _dialog.Show();
            }
            if (view.Id == AddInvoiceIssueDate.Id)
            {
                _dialog.DatePicker.Tag = AddInvoiceIssueDate;
                _dialog.Show();
            }
        }

        public override async Task OnAddButtonClick(CancellationToken token)
        {
            await AddInvoice(token);
        }
    }
}