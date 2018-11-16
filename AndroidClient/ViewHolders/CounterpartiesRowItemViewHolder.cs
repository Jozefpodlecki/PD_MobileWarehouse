using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class CounterpartiesRowItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView CounterpartiesRowItemName { get; set; }
        public TextView CounterpartiesRowItemNIP { get; set; }

        public CounterpartiesRowItemViewHolder(View itemView) : base(itemView)
        {
            CounterpartiesRowItemName = itemView.FindViewById<CheckBox>(Resource.Id.CounterpartiesRowItemName);
            CounterpartiesRowItemNIP = itemView.FindViewById<CheckBox>(Resource.Id.CounterpartiesRowItemNIP);
        }
    }
}