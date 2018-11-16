
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Add
{
    public class GoodsDispatchedNote : Fragment,
        View.IOnClickListener
    {
        public new MainActivity Activity => (MainActivity)base.Activity;
        public Button AddGoodsDispatchedNoteButton { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddGoodsDispatchedNote, container, false);

            AddGoodsDispatchedNoteButton = view.FindViewById<Button>(Resource.Id.AddGoodsReceivedNoteDate);

            AddGoodsDispatchedNoteButton.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View view)
        {
            Activity.NavigationManager.GoToGoodsDispatchedNotes();
        }
    }
}