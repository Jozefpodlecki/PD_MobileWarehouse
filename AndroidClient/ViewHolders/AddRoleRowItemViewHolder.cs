using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class CheckBoxRowItemViewHolder : Java.Lang.Object
    {
        public CheckBox Permission { get; set; }
        public int Position { get; set; }

        public CheckBoxRowItemViewHolder(View itemView)
        {
            Permission = itemView.FindViewById<CheckBox>(Resource.Id.CheckBoxRowItem);
        }

    }
}