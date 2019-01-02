using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Details
{
    public class Counterparty : BaseFragment
    {
        public TextView CounterpartiesDetailsName { get; set; }
        public TextView CounterpartiesDetailsCity { get; set; }
        public TextView CounterpartiesDetailsStreet { get; set; }
        public TextView CounterpartiesDetailsPostalCode { get; set; }
        public TextView CounterpartiesDetailsNIP { get; set; }
        public TextView CounterpartiesDetailsPhoneNumber { get; set; }
        public Models.Counterparty Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CounterpartiesDetails, container, false);
            CounterpartiesDetailsName = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsName);
            CounterpartiesDetailsCity = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsCity);
            CounterpartiesDetailsStreet = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsStreet);
            CounterpartiesDetailsPostalCode = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsPostalCode);
            CounterpartiesDetailsNIP = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsNIP);
            CounterpartiesDetailsPhoneNumber = view.FindViewById<TextView>(Resource.Id.CounterpartiesDetailsPhoneNumber);

            Entity = (Models.Counterparty)Arguments.GetParcelable(Constants.Entity);

            CounterpartiesDetailsName.Text = Entity.Name;
            CounterpartiesDetailsCity.Text = Entity.City.Name;
            CounterpartiesDetailsStreet.Text = Entity.Street;
            CounterpartiesDetailsPostalCode.Text = Entity.PostalCode;
            CounterpartiesDetailsNIP.Text = Entity.NIP;
            CounterpartiesDetailsPhoneNumber.Text = Entity.PhoneNumber;

            return view;
        }
    }
}