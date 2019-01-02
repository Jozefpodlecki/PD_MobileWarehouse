using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Helpers;
using Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Android.App.DatePickerDialog;

namespace Client.Fragments.Add
{
    public class GoodsDispatchedNote : BaseFragment,
        View.IOnClickListener,
        IOnDateSetListener,
        View.IOnFocusChangeListener
    {
        public EditText AddGoodsDispatchedNoteDocumentId { get; set; }
        public EditText AddGoodsDispatchedNoteIssueDate { get; set; }
        public EditText AddGoodsDispatchedNoteDispatchDate { get; set; }
        public AutoCompleteTextView AddGoodsDispatchedNoteInvoiceId { get; set; }
        public Button AddGoodsDispatchedNoteButton { get; set; }
        private BaseArrayAdapter<Models.Invoice> _invoiceAdapter;
        private DatePickerDialog _dialog;
        private AddGoodsDispatchedNoteAdapter _addGoodsDispatchedNoteAdapter;
        public InvoiceFilterCriteria InvoiceFilterCriteria { get; set; }
        public Models.GoodsDispatchedNote Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddGoodsDispatchedNote, container, false);

            AddGoodsDispatchedNoteDocumentId = view.FindViewById<EditText>(Resource.Id.AddGoodsDispatchedNoteDocumentId);
            AddGoodsDispatchedNoteIssueDate = view.FindViewById<EditText>(Resource.Id.AddGoodsDispatchedNoteIssueDate);
            AddGoodsDispatchedNoteDispatchDate = view.FindViewById<EditText>(Resource.Id.AddGoodsDispatchedNoteDispatchDate);
            AddGoodsDispatchedNoteButton = view.FindViewById<Button>(Resource.Id.AddGoodsDispatchedNoteButton);
            AddGoodsDispatchedNoteButton.SetOnClickListener(this);

            AddGoodsDispatchedNoteInvoiceId.AfterTextChanged += AfterTextChanged;
            AddGoodsDispatchedNoteIssueDate.OnFocusChangeListener = this;
            AddGoodsDispatchedNoteDispatchDate.OnFocusChangeListener = this;
            _dialog = CreateDatePickerDialog(this);

            _addGoodsDispatchedNoteAdapter = new AddGoodsDispatchedNoteAdapter(Context, LocationService);
            _invoiceAdapter = new BaseArrayAdapter<Models.Invoice>(Context);

            InvoiceFilterCriteria = new InvoiceFilterCriteria
            {
                ItemsPerPage = 5,
                InvoiceType = InvoiceType.Sales
            };

            var currentDate = DateTime.Now;
            var currentDateFormat = currentDate.ToLongDateString();

            AddGoodsDispatchedNoteIssueDate.Text = currentDateFormat;
            AddGoodsDispatchedNoteIssueDate.Tag = new JavaObjectWrapper<DateTime>(currentDate);
            AddGoodsDispatchedNoteDispatchDate.Text = currentDateFormat;
            AddGoodsDispatchedNoteDispatchDate.Tag = new JavaObjectWrapper<DateTime>(currentDate);
            AddGoodsDispatchedNoteDocumentId.Text = string.Format("WZ/{0:yyyyMMddhhmmss}", currentDate);

            Entity = new Models.GoodsDispatchedNote();

            return view;
        }

        private void OnAutocompleteInvoiceClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var item = _invoiceAdapter.GetItem(eventArgs.Position);
            AddGoodsDispatchedNoteInvoiceId.Tag = item;

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

            _addGoodsDispatchedNoteAdapter.UpdateList(items);
        }

        public void OnFocusChange(View view, bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }

            if (view.Id == AddGoodsDispatchedNoteIssueDate.Id)
            {
                _dialog.DatePicker.Tag = AddGoodsDispatchedNoteIssueDate;
                _dialog.Show();
            }
            if (view.Id == AddGoodsDispatchedNoteDispatchDate.Id)
            {
                _dialog.DatePicker.Tag = AddGoodsDispatchedNoteDispatchDate;
                _dialog.Show();
            }
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            var targetView = view.Tag as EditText;

            var datetime = new DateTime(year, month, dayOfMonth, Calendar);
            targetView.Tag = new JavaObjectWrapper<DateTime>(datetime);
            targetView.Text = datetime.ToLongDateString();
            targetView.ClearFocus();
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;

            if (editText.Id == AddGoodsDispatchedNoteInvoiceId.Id)
            {
                var token = CancelAndSetTokenForView(AddGoodsDispatchedNoteInvoiceId);

                Criteria.Name = eventArgs.Editable.ToString();

                Task.Run(async () =>
                {
                    await GetInvoices(token);
                }, token);
            }
        }

        public async Task GetInvoices(CancellationToken token)
        {
            var result = await InvoiceService.GetInvoices(InvoiceFilterCriteria);

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
            var issueDate = AddGoodsDispatchedNoteIssueDate.Tag as JavaObjectWrapper<DateTime>;
            var dispatchDate = AddGoodsDispatchedNoteDispatchDate.Tag as JavaObjectWrapper<DateTime>;

            if (!ValidateRequired(AddGoodsDispatchedNoteDocumentId))
            {
                return;
            }

            Entity.DocumentId = AddGoodsDispatchedNoteDocumentId.Text;

            if (issueDate == null)
            {
                ShowToastMessage("You have to provide issue date");

                return;
            }

            Entity.IssueDate = issueDate.Data;

            if (issueDate == null)
            {
                ShowToastMessage("You have to provide dispatch date");

                return;
            }

            Entity.DispatchDate = dispatchDate.Data;

            if (AddGoodsDispatchedNoteInvoiceId.Tag == null)
            {
                ShowToastMessage("You have to provide invoice id");

                return;
            }

            Entity.NoteEntry = _addGoodsDispatchedNoteAdapter.Items;

            if (Entity.NoteEntry.Any(ne => ne.Location == null))
            {
                ShowToastMessage("You have to fill all locations for products");

                return;
            }

            var token = CancelAndSetTokenForView(view);

            Task.Run(async () =>
            {
                var result = await NoteService.AddGoodsDispatchedNote(Entity, token);

                if (result.Error.Any())
                {
                    ShowToastMessage("An error occurred");

                    return;
                }

                RunOnUiThread(() =>
                {
                    NavigationManager.GoToGoodsDispatchedNotes();
                });
            });
        }
    }
}