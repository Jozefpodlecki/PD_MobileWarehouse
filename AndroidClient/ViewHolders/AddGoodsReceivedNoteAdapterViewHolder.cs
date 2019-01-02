using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class AddGoodsReceivedNoteAdapterViewHolder : Java.Lang.Object
    {
        public TextView AddGoodsReceivedNoteRowItemProductName { get; set; }
        public AutoCompleteTextView AddGoodsReceivedNoteRowItemLocation { get; set; }
        public int Position { get; set; }

        public AddGoodsReceivedNoteAdapterViewHolder(View view)
        {
            AddGoodsReceivedNoteRowItemProductName = view.FindViewById<TextView>(Resource.Id.AddGoodsReceivedNoteRowItemProductName);
            AddGoodsReceivedNoteRowItemLocation = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddGoodsReceivedNoteRowItemLocation);

            AddGoodsReceivedNoteRowItemProductName.Tag = this;
            AddGoodsReceivedNoteRowItemLocation.Tag = this;
        }
    }
}