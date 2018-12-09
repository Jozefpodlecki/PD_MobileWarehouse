using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Models;

namespace Client.Fragments.Add
{
    public class GoodsReceivedNote : BaseFragment,
        View.IOnClickListener
    {
        public TextView AddGoodsReceivedNoteDocumentId { get; set; }
        public EditText AddGoodsReceivedNoteDate { get; set; }
        public AutoCompleteTextView AddGoodsReceivedNoteInvoiceId { get; set; }
        public ListView AddGoodsReceivedNoteProducts { get; set; }
        public Button AddGoodsReceivedNoteButton { get; set; }
        public Button AddGoodsReceivedNoteAddProduct { get; set; }
        private BaseArrayAdapter<Models.Invoice> _invoiceAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddGoodsReceivedNote, container, false);

            //AddGoodsReceivedNoteDocumentId = view.FindViewById<TextView>(Resource.Id.AddGoodsReceivedNoteDocumentId);
            AddGoodsReceivedNoteDate = view.FindViewById<EditText>(Resource.Id.AddGoodsReceivedNoteDate);
            AddGoodsReceivedNoteInvoiceId = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddGoodsReceivedNoteInvoiceId);
            AddGoodsReceivedNoteProducts = view.FindViewById<ListView>(Resource.Id.AddGoodsReceivedNoteProducts);
            AddGoodsReceivedNoteButton = view.FindViewById<Button>(Resource.Id.AddGoodsReceivedNoteButton);
            //AddGoodsReceivedNoteAddProduct = view.FindViewById<Button>(Resource.Id.AddGoodsReceivedNoteAddProduct);

            AddGoodsReceivedNoteInvoiceId.AfterTextChanged += afterTextChanged;
            AddGoodsReceivedNoteButton.SetOnClickListener(this);
            //AddGoodsReceivedNoteAddProduct.SetOnClickListener(this);

            var items = new List<AddGoodsReceivedNoteEntry>();

            items.Add(new AddGoodsReceivedNoteEntry
            {

            });

            var adapter = new AddGoodsReceivedNoteAdapter(Context,items);

            AddGoodsReceivedNoteInvoiceId.Threshold = 1;
            _invoiceAdapter = new BaseArrayAdapter<Models.Invoice>(Activity);
            AddGoodsReceivedNoteInvoiceId.Adapter = _invoiceAdapter;

            Criteria.ItemsPerPage = 10;

            //AddGoodsReceivedNoteProducts.Adapter = adapter;

            return view;
        }

        private void afterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {


            if (sender == AddGoodsReceivedNoteInvoiceId)
            {
                var token = CancelAndSetTokenForView(AddGoodsReceivedNoteInvoiceId);

                Criteria.Name = eventArgs.Editable.ToString();

                var task = Task.Run(GetInvoices, token);
            }
        }

        public async Task GetInvoices()
        {
            var result = await NoteService.GetInvoices(Criteria);

            Activity.RunOnUiThread(() =>
            {
                if (result.Error != null)
                {
                    ShowToastMessage("An error occurred");

                    return;
                }

                _invoiceAdapter.UpdateList(result.Data);
            });
        }

        public void OnClick(View view)
        {


            var token = CancelAndSetTokenForView(view);

            var note = new Common.DTO.GoodsDispatchedNote
            {
                
            };

            NoteService.AddGoodsDispatchedNote(note);

            NavigationManager.GoToGoodsReceivedNotes();
        }
    }
}