using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class AddGoodsReceivedNoteAdapterViewHolder : Java.Lang.Object
    {
        public AutoCompleteTextView AddGoodsReceivedNoteRowItemProductName { get; set; }
        public EditText AddGoodsReceivedNoteRowItemAmount { get; set; }
        public EditText AddGoodsReceivedNoteRowItemPrice { get; set; }
        public Button AddGoodsReceivedNoteRowItemDelete { get; set; }

        public AddGoodsReceivedNoteAdapterViewHolder(ViewGroup viewGroup)
        {
            AddGoodsReceivedNoteRowItemProductName = viewGroup.FindViewById<AutoCompleteTextView>(Resource.Id.AddGoodsReceivedNoteRowItemProductName);
            AddGoodsReceivedNoteRowItemAmount = viewGroup.FindViewById<EditText>(Resource.Id.AddGoodsReceivedNoteRowItemAmount);
            AddGoodsReceivedNoteRowItemPrice = viewGroup.FindViewById<EditText>(Resource.Id.AddGoodsReceivedNoteRowItemPrice);
            AddGoodsReceivedNoteRowItemDelete = viewGroup.FindViewById<Button>(Resource.Id.AddGoodsReceivedNoteRowItemDelete);
        }
    }
}