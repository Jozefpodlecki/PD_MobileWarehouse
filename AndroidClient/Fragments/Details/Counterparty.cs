using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Details
{
    public class Counterparty : BaseDetailsFragment<Models.Counterparty>
    {
        public TextView CounterpartiesDetailsName { get; set; }
        public TextView CounterpartiesDetailsCity { get; set; }
        public TextView CounterpartiesDetailsStreet { get; set; }
        public TextView CounterpartiesDetailsPostalCode { get; set; }
        public TextView CounterpartiesDetailsNIP { get; set; }
        public TextView CounterpartiesDetailsPhoneNumber { get; set; }

        public Counterparty() : base(Resource.Layout.CounterpartiesDetails)
        {
        }

        public override void OnBindElements(View view)
        {
            CounterpartiesDetailsName = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsName);
            CounterpartiesDetailsCity = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsCity);
            CounterpartiesDetailsStreet = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsStreet);
            CounterpartiesDetailsPostalCode = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsPostalCode);
            CounterpartiesDetailsNIP = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsNIP);
            CounterpartiesDetailsPhoneNumber = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsPhoneNumber);
            
            CounterpartiesDetailsName.Text = Entity.Name;
            CounterpartiesDetailsCity.Text = Entity.City.Name;
            CounterpartiesDetailsStreet.Text = Entity.Street;
            CounterpartiesDetailsPostalCode.Text = Entity.PostalCode;
            CounterpartiesDetailsNIP.Text = Entity.NIP;
            CounterpartiesDetailsPhoneNumber.Text = Entity.PhoneNumber;
        }
    }
}