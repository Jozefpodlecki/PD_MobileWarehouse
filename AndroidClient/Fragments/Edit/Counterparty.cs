using Android.OS;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Fragments.Edit
{
    public class Counterparty : BaseFragment,
        View.IOnClickListener
    {
        private BaseArrayAdapter<City> _cityAdapter;

        public Button SaveCounterpartyButton { get; set; }
        public TextInputEditText CounterpartyEditName { get; set; }
        public AutoCompleteTextView CounterpartyEditCity { get; set; }
        public TextInputEditText CounterpartyEditStreet { get; set; }
        public TextInputEditText CounterpartyEditPostalCode { get; set; }
        public TextInputEditText CounterpartyEditNIP { get; set; }
        public TextInputEditText CounterpartyEditPhoneNumber { get; set; }
        public Models.Counterparty Entity { get; set; }

        public static Dictionary<int, Action<Models.Counterparty, string>> ViewToObjectMap = new Dictionary<int, Action<Models.Counterparty, string>>()
        {
            { Resource.Id.CounterpartyEditName, (model, text) => { model.Name = text; } },
            { Resource.Id.CounterpartyEditStreet, (model, text) => { model.Street = text; } },
            { Resource.Id.CounterpartyEditNIP, (model, text) => { model.NIP = text; } },
            { Resource.Id.CounterpartyEditPostalCode, (model, text) => { model.PostalCode = text; } },
            { Resource.Id.CounterpartyEditPhoneNumber, (model, text) => { model.PhoneNumber = text; } },
            { Resource.Id.CounterpartyEditCity, (model, text) => { model.City.Name = text; model.City.Id = 0; } }
        };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CounterpartiesEdit, container, false);
            CounterpartyEditName = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditName);
            CounterpartyEditCity = view.FindViewById<AutoCompleteTextView>(Resource.Id.CounterpartyEditCity);
            CounterpartyEditStreet = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditStreet);
            CounterpartyEditPostalCode = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditPostalCode);
            CounterpartyEditNIP = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditNIP);
            CounterpartyEditPhoneNumber = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditPhoneNumber);
            SaveCounterpartyButton = view.FindViewById<Button>(Resource.Id.SaveCounterpartyButton);

            CounterpartyEditName.AfterTextChanged += afterTextChanged;
            CounterpartyEditStreet.AfterTextChanged += afterTextChanged;
            CounterpartyEditCity.AfterTextChanged += afterTextChanged;
            CounterpartyEditPostalCode.AfterTextChanged += afterTextChanged;
            CounterpartyEditPhoneNumber.AfterTextChanged += afterTextChanged;
            CounterpartyEditNIP.AfterTextChanged += afterTextChanged;
            SaveCounterpartyButton.SetOnClickListener(this);

            Entity = (Models.Counterparty)Arguments.GetParcelable(Constants.Entity);
            CounterpartyEditName.Text = Entity.Name;
            CounterpartyEditStreet.Text = Entity.Street;
            CounterpartyEditPostalCode.Text = Entity.PostalCode;
            CounterpartyEditNIP.Text = Entity.NIP;
            CounterpartyEditPhoneNumber.Text = Entity.PhoneNumber;

            var token = CancelAndSetTokenForView(CounterpartyEditCity);

            _cityAdapter = new BaseArrayAdapter<Models.City>(Activity);

            CounterpartyEditCity.Adapter = _cityAdapter;
            CounterpartyEditCity.Threshold = 1;
            Criteria.ItemsPerPage = 10;

            var task = Task.Run(GetCities, token);

            CounterpartyEditCity.ItemClick += OnAutocompleteCounterpartyClick;

            CounterpartyEditCity.Text = Entity.City.Name;

            return view;
        }

        private void OnAutocompleteCounterpartyClick(object adapter, AdapterView.ItemClickEventArgs eventArgs)
        {
            var item = _cityAdapter.GetItem(eventArgs.Position);
            Entity.City.Id = item.Id;
            Entity.City.Name = item.Name;
        }

        private void afterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            Validate();

            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            ViewToObjectMap[editText.Id](Entity, text);

            if (editText.Id == Resource.Id.AddCounterpartyCity)
            {
                var token = CancelAndSetTokenForView(CounterpartyEditCity);

                Criteria.Name = text;

                var task = Task.Run(GetCities, token);
            }
        }

        private void Validate()
        {
            SaveCounterpartyButton.Enabled =
                !string.IsNullOrEmpty(CounterpartyEditName.Text) &&
                !string.IsNullOrEmpty(CounterpartyEditStreet.Text) &&
                !string.IsNullOrEmpty(CounterpartyEditCity.Text) &&
                !string.IsNullOrEmpty(CounterpartyEditNIP.Text);
        }

        public async Task GetCities()
        {
            var result = await CityService.GetCities(Criteria);

            RunOnUiThread(() =>
            {
                if (result.Error.Any())
                {
                    ShowToastMessage("An error occurred");

                    return;
                }

                _cityAdapter.UpdateList(result.Data);
            });
        }

        public void OnClick(View view)
        {
            var token = CancelAndSetTokenForView(CounterpartyEditCity);
            CounterpartyEditCity.Enabled = false;

            Task.Run(async () =>
            {
                var result = await CounterpartyService.UpdateCounterparty(Entity, token);

                if (result.Error.Any())
                {
                    RunOnUiThread(() =>
                    {
                        ShowToastMessage(Resource.String.ErrorOccurred);
                        SaveCounterpartyButton.Enabled = true;

                    });

                    return;
                }

                NavigationManager.GoToCounterparties();

            }, token);
        }
    }
}