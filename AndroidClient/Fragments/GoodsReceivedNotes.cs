
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
using static Android.Support.V7.Widget.RecyclerView;

namespace Client.Fragments
{
    public class GoodsReceivedNotes : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public RecyclerView GoodReceivedNotesList { get; set; }
        public FloatingActionButton AddGoodsReceivedNotesButton { get; set; }
        public AutoCompleteTextView SearchGoodReceivedNote { get; set; }
        public LayoutManager _layoutManager;
        private GoodsReceivedNotesAdapter _adapter;
        public TextView EmptyView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.GoodsReceivedNotes, container, false);

            GoodReceivedNotesList = view.FindViewById<RecyclerView>(Resource.Id.GoodReceivedNotesList);
            AddGoodsReceivedNotesButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddGoodReceivedNoteFloatActionButton);
            SearchGoodReceivedNote = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchGoodReceivedNote);

            AddGoodsReceivedNotesButton.SetOnClickListener(this);
            //_noteService = new NoteService(Activity);
            //_adapter = new GoodsReceivedNotesAdapter();



            return view;
        }

        public void OnClick(View view)
        {
            NavigationManager.GoToAddGoodsReceivedNote();
        }

        public void AfterTextChanged(IEditable s)
        {
        }
        
    }
}