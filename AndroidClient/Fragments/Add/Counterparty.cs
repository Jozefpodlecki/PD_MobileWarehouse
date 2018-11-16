
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Add
{
    public class Counterparty : Fragment,
        View.IOnClickListener
    {
        public EditText AddCounterpartyName { get; set; }
        public EditText AddCounterpartyStreet { get; set; }
        public EditText AddCounterpartyCity { get; set; }
        public EditText AddCounterpartyPhone { get; set; }
        public EditText AddCounterpartyNIP { get; set; }
        public Button AddCounterpartyButton { get; set; }
        public new MainActivity Activity => (MainActivity)base.Activity;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddCounterparty, container, false);

            AddCounterpartyName = view.FindViewById<EditText>(Resource.Id.AddCounterpartyName);
            AddCounterpartyStreet = view.FindViewById<EditText>(Resource.Id.AddCounterpartyStreet);
            AddCounterpartyCity = view.FindViewById<EditText>(Resource.Id.AddCounterpartyCity);
            AddCounterpartyPhone = view.FindViewById<EditText>(Resource.Id.AddCounterpartyPhone);
            AddCounterpartyNIP = view.FindViewById<EditText>(Resource.Id.AddCounterpartyNIP);
            AddCounterpartyButton = view.FindViewById<Button>(Resource.Id.AddCounterpartyButton);

            AddCounterpartyButton.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View view)
        {
            Activity.NavigationManager.GoToCounterparties();
        }

    }
}