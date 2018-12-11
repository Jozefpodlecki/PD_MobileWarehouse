using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class CounterpartiesRowItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView CounterpartiesRowItemName { get; set; }
        public TextView CounterpartiesRowItemNIP { get; set; }
        public ImageButton CounterpartiesRowItemInfo { get; set; }
        public ImageButton CounterpartiesRowItemEdit { get; set; }
        public ImageButton CounterpartiesRowItemDelete { get; set; }

        public CounterpartiesRowItemViewHolder(View itemView) : base(itemView)
        {
            CounterpartiesRowItemName = itemView.FindViewById<TextView>(Resource.Id.CounterpartiesRowItemName);
            CounterpartiesRowItemNIP = itemView.FindViewById<TextView>(Resource.Id.CounterpartiesRowItemNIP);
            CounterpartiesRowItemInfo = itemView.FindViewById<ImageButton>(Resource.Id.CounterpartiesRowItemInfo);
            CounterpartiesRowItemEdit = itemView.FindViewById<ImageButton>(Resource.Id.CounterpartiesRowItemEdit);
            CounterpartiesRowItemDelete = itemView.FindViewById<ImageButton>(Resource.Id.CounterpartiesRowItemDelete);

            CounterpartiesRowItemInfo.Tag = this;
            CounterpartiesRowItemEdit.Tag = this;
            CounterpartiesRowItemDelete.Tag = this;
        }
    }
}