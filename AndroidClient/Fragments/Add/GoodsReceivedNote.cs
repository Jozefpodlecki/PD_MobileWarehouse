using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Helpers;
using Common;
using static Android.App.DatePickerDialog;

namespace Client.Fragments.Add
{
    public class GoodsReceivedNote : BaseFragment,
        View.IOnClickListener,
        IOnDateSetListener,
        View.IOnFocusChangeListener
    {

        public EditText AddGoodsReceivedNoteDocumentId { get; set; }
        public EditText AddGoodsReceivedNoteIssueDate { get; set; }
        public EditText AddGoodsReceivedNoteReceiveDate { get; set; }
        public AutoCompleteTextView AddGoodsReceivedNoteInvoiceId { get; set; }
        public ListView AddGoodsReceivedNoteProducts { get; set; }
        public Button AddGoodsReceivedNoteButton { get; set; }
        private BaseArrayAdapter<Models.Invoice> _invoiceAdapter;
        private DatePickerDialog _dialog;
        private AddGoodsReceivedNoteAdapter _addGoodsReceivedNoteAdapter;
        public Models.GoodsReceivedNote Entity { get; set; }
        public InvoiceFilterCriteria InvoiceFilterCriteria { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddGoodsReceivedNote, container, false);

            AddGoodsReceivedNoteDocumentId = view.FindViewById<EditText>(Resource.Id.AddGoodsReceivedNoteDocumentId);
            AddGoodsReceivedNoteIssueDate = view.FindViewById<EditText>(Resource.Id.AddGoodsReceivedNoteIssueDate);
            AddGoodsReceivedNoteReceiveDate = view.FindViewById<EditText>(Resource.Id.AddGoodsReceivedNoteReceiveDate);
            AddGoodsReceivedNoteInvoiceId = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddGoodsReceivedNoteInvoiceId);
            AddGoodsReceivedNoteProducts = view.FindViewById<ListView>(Resource.Id.AddGoodsReceivedNoteProducts);
            AddGoodsReceivedNoteButton = view.FindViewById<Button>(Resource.Id.AddGoodsReceivedNoteButton);

            AddGoodsReceivedNoteInvoiceId.AfterTextChanged += AfterTextChanged;
            AddGoodsReceivedNoteButton.SetOnClickListener(this);
            AddGoodsReceivedNoteIssueDate.OnFocusChangeListener = this;
            AddGoodsReceivedNoteReceiveDate.OnFocusChangeListener = this;

            _dialog = CreateDatePickerDialog(this);

            _addGoodsReceivedNoteAdapter = new AddGoodsReceivedNoteAdapter(Context, LocationService);
            _invoiceAdapter = new BaseArrayAdapter<Models.Invoice>(Context);

            AddGoodsReceivedNoteInvoiceId.Threshold = 1;
            AddGoodsReceivedNoteInvoiceId.Adapter = _invoiceAdapter;
            AddGoodsReceivedNoteProducts.Adapter = _addGoodsReceivedNoteAdapter;
            AddGoodsReceivedNoteInvoiceId.ItemClick += OnAutocompleteInvoiceClick;

            InvoiceFilterCriteria = new InvoiceFilterCriteria
            {
                ItemsPerPage = 5,
                InvoiceType = InvoiceType.Purchase
            };

            var currentDate = DateTime.Now;
            var currentDateFormat = currentDate.ToLongDateString();

            AddGoodsReceivedNoteIssueDate.Text = currentDateFormat;
            AddGoodsReceivedNoteIssueDate.Tag = new JavaObjectWrapper<DateTime>(currentDate);
            AddGoodsReceivedNoteReceiveDate.Text = currentDateFormat;
            AddGoodsReceivedNoteReceiveDate.Tag = new JavaObjectWrapper<DateTime>(currentDate);
            AddGoodsReceivedNoteDocumentId.Text = string.Format("PZ/{0:yyyyMMddhhmmss}", currentDate);

            Entity = new Models.GoodsReceivedNote();

            return view;
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

            if (view.Id == AddGoodsReceivedNoteIssueDate.Id)
            {
                _dialog.DatePicker.Tag = AddGoodsReceivedNoteIssueDate;
                _dialog.Show();
            }
            if (view.Id == AddGoodsReceivedNoteReceiveDate.Id)
            {
                _dialog.DatePicker.Tag = AddGoodsReceivedNoteReceiveDate;
                _dialog.Show();
            }
        }

        private void OnAutocompleteInvoiceClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var item = _invoiceAdapter.GetItem(eventArgs.Position);
            AddGoodsReceivedNoteInvoiceId.Tag = item;

            if (item.GoodsReceivedNote != null)
            {
                return;
            }

            var items = item
                .Products
                .Select(pr => new Models.NoteEntry
                {
                    Name = pr.Name
                })
                .ToList();

            _addGoodsReceivedNoteAdapter.UpdateList(items);
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;

            if (editText.Id == AddGoodsReceivedNoteInvoiceId.Id)
            {
                var token = CancelAndSetTokenForView(AddGoodsReceivedNoteInvoiceId);

                Criteria.Name = eventArgs.Editable.ToString();

                Task.Run(async () =>
                {
                    await GetInvoices(token);
                }, token);
            }
        }

        public async Task GetInvoices(CancellationToken token)
        {
            var result = await InvoiceService.GetInvoices(InvoiceFilterCriteria, token);

            RunOnUiThread(() =>
            {
                if (result.Error.Any())
                {
                    ShowToastMessage("An error occurred");

                    return;
                }

                _invoiceAdapter.UpdateList(result.Data);
            });
        }

        public void OnClick(View view)
        {
            var issueDate = AddGoodsReceivedNoteIssueDate.Tag as JavaObjectWrapper<DateTime>;
            var receiveDate = AddGoodsReceivedNoteReceiveDate.Tag as JavaObjectWrapper<DateTime>;

            if (!ValidateRequired(AddGoodsReceivedNoteDocumentId))
            {
                return;
            }

            Entity.DocumentId = AddGoodsReceivedNoteDocumentId.Text;

            if (issueDate == null)
            {
                ShowToastMessage("You have to provide issue date");

                return;
            }

            Entity.IssueDate = issueDate.Data;

            if (receiveDate == null)
            {
                ShowToastMessage("You have to provide receive date");

                return;
            }

            Entity.ReceiveDate = receiveDate.Data;

            if (AddGoodsReceivedNoteInvoiceId.Tag == null)
            {
                ShowToastMessage("You have to provide invoice id"); 

                return;
            }

            Entity.Invoice = (Models.Invoice)AddGoodsReceivedNoteInvoiceId.Tag;
            Entity.InvoiceId = Entity.Invoice.Id;
            Entity.NoteEntry = _addGoodsReceivedNoteAdapter.Items;

            if (Entity.NoteEntry.Any(ne => ne.Location == null))
            {
                ShowToastMessage(Resource.String.GoodReceivedNoteInvalidLocations);

                return;
            }

            var token = CancelAndSetTokenForView(view);

            Task.Run(async () =>
            {
                var result = await NoteService.AddGoodsReceivedNote(Entity, token);

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
                    NavigationManager.GoToGoodsReceivedNotes();
                });
            });
        }
    }
}