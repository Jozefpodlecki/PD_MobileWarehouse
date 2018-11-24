using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Client.Adapters;

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
        public new MainActivity Activity => (MainActivity)base.Activity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddGoodsReceivedNote, container, false);

            AddGoodsReceivedNoteDocumentId = view.FindViewById<TextView>(Resource.Id.AddGoodsReceivedNoteDocumentId);
            AddGoodsReceivedNoteDate = view.FindViewById<EditText>(Resource.Id.AddGoodsReceivedNoteDate);
            AddGoodsReceivedNoteInvoiceId = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddGoodsReceivedNoteInvoiceId);
            AddGoodsReceivedNoteProducts = view.FindViewById<ListView>(Resource.Id.AddGoodsReceivedNoteProducts);
            AddGoodsReceivedNoteButton = view.FindViewById<Button>(Resource.Id.AddGoodsReceivedNoteButton);
            AddGoodsReceivedNoteAddProduct = view.FindViewById<Button>(Resource.Id.AddGoodsReceivedNoteAddProduct);

            AddGoodsReceivedNoteButton.SetOnClickListener(this);
            AddGoodsReceivedNoteAddProduct.SetOnClickListener(this);

            var items = new List<AddGoodsReceivedNoteEntry>();

            items.Add(new AddGoodsReceivedNoteEntry
            {

            });

            items.Add(new AddGoodsReceivedNoteEntry
            {

            });

            items.Add(new AddGoodsReceivedNoteEntry
            {

            });

            var adapter = new AddGoodsReceivedNoteAdapter(Context,items);

            AddGoodsReceivedNoteProducts.Adapter = adapter;

            return view;
        }

        public void OnClick(View view)
        {
            NavigationManager.GoToGoodsReceivedNotes();
        }
    }
}