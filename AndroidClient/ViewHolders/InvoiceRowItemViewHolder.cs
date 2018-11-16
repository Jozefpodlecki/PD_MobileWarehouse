using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class InvoiceRowItemViewHolder : RecyclerView.ViewHolder
    {
        public EditText InvoiceId { get; set; }
        public EditText Price { get; set; }

        public InvoiceRowItemViewHolder(View itemView) : base(itemView)
        {
            InvoiceId = itemView.FindViewById<EditText>(Resource.Id.CounterpartiesRowItemName);
            Price = itemView.FindViewById<EditText>(Resource.Id.CounterpartiesRowItemName);
        }
    }
}