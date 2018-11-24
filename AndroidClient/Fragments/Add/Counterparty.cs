
using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Filters;
using Client.Services;
using Common.DTO;
using Java.Lang;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Fragments.Add
{
    public class Counterparty : BaseFragment,
        View.IOnClickListener,
        View.IOnFocusChangeListener
    {
        public EditText AddCounterpartyName { get; set; }
        public EditText AddCounterpartyStreet { get; set; }
        public AutoCompleteTextView AddCounterpartyCity { get; set; }
        public EditText AddCounterpartyPhone { get; set; }
        public EditText AddCounterpartyNIP { get; set; }
        public Button AddCounterpartyButton { get; set; }
        public BaseArrayAdapter<Models.City> _cityAdapter;
        private Common.DTO.City _city;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddCounterparty, container, false);

            AddCounterpartyName = view.FindViewById<EditText>(Resource.Id.AddCounterpartyName);
            AddCounterpartyStreet = view.FindViewById<EditText>(Resource.Id.AddCounterpartyStreet);
            AddCounterpartyCity = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddCounterpartyCity);
            AddCounterpartyPhone = view.FindViewById<EditText>(Resource.Id.AddCounterpartyPhone);
            AddCounterpartyNIP = view.FindViewById<EditText>(Resource.Id.AddCounterpartyNIP);
            AddCounterpartyButton = view.FindViewById<Button>(Resource.Id.AddCounterpartyButton);
           
            AddCounterpartyName.AfterTextChanged += afterTextChanged;
            AddCounterpartyStreet.AfterTextChanged += afterTextChanged;
            AddCounterpartyCity.AfterTextChanged += afterTextChanged;
            AddCounterpartyPhone.AfterTextChanged += afterTextChanged;
            AddCounterpartyNIP.AfterTextChanged += afterTextChanged;
            AddCounterpartyName.OnFocusChangeListener = this;
            AddCounterpartyButton.SetOnClickListener(this);
            
            AddCounterpartyButton.Enabled = false;

            var token = CancelAndSetTokenForView(AddCounterpartyCity);

            _city = new City
            {

            };

            _cityAdapter = new BaseArrayAdapter<Models.City>(Activity);
            
            AddCounterpartyCity.Adapter = _cityAdapter;
            AddCounterpartyCity.Threshold = 1;
            Criteria.ItemsPerPage = 10;

            var task = Task.Run(GetCities, token);

            AddCounterpartyCity.ItemClick += (adapter, eventArgs) =>
            {
                var item = _cityAdapter.GetItem(eventArgs.Position);
                _city.Id = item.Id;
                _city.Name = item.Name;
            };

            return view;
        }

        private void afterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            Validate();

            if(sender == AddCounterpartyCity)
            {
                var token = CancelAndSetTokenForView(AddCounterpartyCity);

                Criteria.Name = eventArgs.Editable.ToString();

                var task = Task.Run(GetCities, token);
            }
            
        }

        public async Task GetCities()
        {
            var result = await NoteService.GetCities(Criteria);

            Activity.RunOnUiThread(() =>
            {
                if(result.Error != null)
                {
                    ShowToastMessage("An error occurred");

                    return;
                }

                _cityAdapter.UpdateList(result.Data);
            });
        }

        private void Validate()
        {
            AddCounterpartyButton.Enabled =
                !string.IsNullOrEmpty(AddCounterpartyName.Text) &&
                !string.IsNullOrEmpty(AddCounterpartyStreet.Text) &&
                !string.IsNullOrEmpty(AddCounterpartyCity.Text) &&
                !string.IsNullOrEmpty(AddCounterpartyNIP.Text);
        }

        public async void OnClick(View view)
        {
            AddCounterpartyButton.Enabled = false;

            if(_city.Name != AddCounterpartyCity.Text)
            {
                _city.Id = 0;
                _city.Name = AddCounterpartyCity.Text;
            }

            var counterparty = new Common.DTO.Counterparty
            {
                Name = AddCounterpartyName.Text,
                Street = AddCounterpartyStreet.Text,
                PostalCode = AddCounterpartyStreet.Text,
                NIP = AddCounterpartyNIP.Text,
                PhoneNumber = AddCounterpartyPhone.Text,
                City = _city
            };

            var result = await NoteService.AddCounterparty(counterparty);

            if(result.Error != null)
            {
                ShowToastMessage("An error occurred");
                AddCounterpartyButton.Enabled = false;

                return;
            }


            NavigationManager.GoToCounterparties();
        }

        public async void OnFocusChange(View view, bool hasFocus)
        {
            return;

            if (view == AddCounterpartyName)
            {
                if (!hasFocus)
                {
                    if(string.IsNullOrEmpty(AddCounterpartyName.Text))
                    {
                        AddCounterpartyName.SetError("Field is required", null);
                        AddCounterpartyName.RequestFocus();

                        AddCounterpartyButton.Enabled = false;

                        return;
                    }

                    var result = await NoteService.CounterpartyExists(AddCounterpartyName.Text);

                    if(result.Error != null)
                    {
                        Toast.MakeText(Context, "An error occurred", ToastLength.Short);

                        return;
                    }

                    if (result.Data)
                    {
                        AddCounterpartyName.SetError("Counterparty exists", null);
                        AddCounterpartyName.RequestFocus();

                        AddCounterpartyButton.Enabled = false;

                        return;
                    }

                    AddCounterpartyButton.Enabled = true;

                }
            }
        }
    }
}