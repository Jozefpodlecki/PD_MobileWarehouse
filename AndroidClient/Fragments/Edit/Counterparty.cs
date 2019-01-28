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
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments.Edit
{
    public class Counterparty : BaseEditFragment<Models.Counterparty>
    {
        private BaseArrayAdapter<City> _cityAdapter;
        public TextInputEditText CounterpartyEditName { get; set; }
        public AutoCompleteTextView CounterpartyEditCity { get; set; }
        public TextInputEditText CounterpartyEditStreet { get; set; }
        public TextInputEditText CounterpartyEditPostalCode { get; set; }
        public TextInputEditText CounterpartyEditNIP { get; set; }
        public TextInputEditText CounterpartyEditPhoneNumber { get; set; }

        public Counterparty() : base(Resource.Layout.CounterpartiesEdit)
        {
        }

        public override void OnBindElements(View view)
        {
            CounterpartyEditName = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditName);
            CounterpartyEditCity = view.FindViewById<AutoCompleteTextView>(Resource.Id.CounterpartyEditCity);
            CounterpartyEditStreet = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditStreet);
            CounterpartyEditPostalCode = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditPostalCode);
            CounterpartyEditNIP = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditNIP);
            CounterpartyEditPhoneNumber = view.FindViewById<TextInputEditText>(Resource.Id.CounterpartyEditPhoneNumber);

            CounterpartyEditName.Text = Entity.Name;
            CounterpartyEditStreet.Text = Entity.Street;
            CounterpartyEditCity.Text = Entity.City.Name;
            CounterpartyEditPostalCode.Text = Entity.PostalCode;
            CounterpartyEditNIP.Text = Entity.NIP;
            CounterpartyEditPhoneNumber.Text = Entity.PhoneNumber;

            CounterpartyEditName.AfterTextChanged += afterTextChanged;
            CounterpartyEditStreet.AfterTextChanged += afterTextChanged;
            CounterpartyEditCity.AfterTextChanged += afterTextChanged;
            CounterpartyEditPostalCode.AfterTextChanged += afterTextChanged;
            CounterpartyEditPhoneNumber.AfterTextChanged += afterTextChanged;
            CounterpartyEditNIP.AfterTextChanged += afterTextChanged;

            var token = CancelAndSetTokenForView(CounterpartyEditCity);

            _cityAdapter = new BaseArrayAdapter<Models.City>(Activity);

            CounterpartyEditCity.Adapter = _cityAdapter;
            CounterpartyEditCity.Threshold = 1;
            Criteria.ItemsPerPage = 10;

            var task = Task.Run(GetCities, token);

            CounterpartyEditCity.ItemClick += OnAutocompleteCounterpartyClick;
        }

        public static Dictionary<int, Action<Models.Counterparty, string>> ViewToObjectMap = new Dictionary<int, Action<Models.Counterparty, string>>()
        {
            { Resource.Id.CounterpartyEditName, (model, text) => { model.Name = text; } },
            { Resource.Id.CounterpartyEditStreet, (model, text) => { model.Street = text; } },
            { Resource.Id.CounterpartyEditNIP, (model, text) => { model.NIP = text; } },
            { Resource.Id.CounterpartyEditPostalCode, (model, text) => { model.PostalCode = text; } },
            { Resource.Id.CounterpartyEditPhoneNumber, (model, text) => { model.PhoneNumber = text; } },
            { Resource.Id.CounterpartyEditCity, (model, text) => { model.City.Name = text; model.City.Id = 0; } }
        };

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

        public async Task GetCities()
        {
            var result = await CityService.GetCities(Criteria);

            RunOnUiThread(() =>
            {
                if (result.Error.Any())
                {
                    ShowToastMessage(Resource.String.ErrorOccurred);

                    return;
                }

                _cityAdapter.UpdateList(result.Data);
            });
        }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(CounterpartyEditName.Text) &&
                !string.IsNullOrEmpty(CounterpartyEditStreet.Text) &&
                !string.IsNullOrEmpty(CounterpartyEditCity.Text) &&
                !string.IsNullOrEmpty(CounterpartyEditNIP.Text);
        }

        public override async Task OnSaveButtonClick(CancellationToken token)
        {
            var result = await CounterpartyService.UpdateCounterparty(Entity, token);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    ShowToastMessage(Resource.String.ErrorOccurred);
                    SaveButton.Enabled = true;

                });

                return;
            }

            RunOnUiThread(() =>
            {
                NavigationManager.GoToCounterparties();
            });
        }
    }
}