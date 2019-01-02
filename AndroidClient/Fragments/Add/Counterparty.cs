using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using System;
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
        public EditText AddCounterpartyPostalCode { get; set; }
        public AutoCompleteTextView AddCounterpartyCity { get; set; }
        public EditText AddCounterpartyPhone { get; set; }
        public EditText AddCounterpartyNIP { get; set; }
        public Button AddCounterpartyButton { get; set; }
        public BaseArrayAdapter<Models.City> _cityAdapter;
        public Models.Counterparty Entity { get; set; }

        public static Dictionary<int, Action<Models.Counterparty, string>> ViewToObjectMap = new Dictionary<int, Action<Models.Counterparty, string>>()
        {
            { Resource.Id.AddCounterpartyName, (model, text) => { model.Name = text; } },
            { Resource.Id.AddCounterpartyStreet, (model, text) => { model.Street = text; } },
            { Resource.Id.AddCounterpartyNIP, (model, text) => { model.NIP = text; } },
            { Resource.Id.AddCounterpartyPostalCode, (model, text) => { model.PostalCode = text; } },
            { Resource.Id.AddCounterpartyPhone, (model, text) => { model.PhoneNumber = text; } },
            { Resource.Id.AddCounterpartyCity, (model, text) => { model.City.Name = text; model.City.Id = 0; } }
        };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddCounterparty, container, false);

            AddCounterpartyName = view.FindViewById<EditText>(Resource.Id.AddCounterpartyName);
            AddCounterpartyStreet = view.FindViewById<EditText>(Resource.Id.AddCounterpartyStreet);
            AddCounterpartyPostalCode = view.FindViewById<EditText>(Resource.Id.AddCounterpartyPostalCode);
            AddCounterpartyCity = view.FindViewById<AutoCompleteTextView>(Resource.Id.AddCounterpartyCity);
            AddCounterpartyPhone = view.FindViewById<EditText>(Resource.Id.AddCounterpartyPhone);
            AddCounterpartyNIP = view.FindViewById<EditText>(Resource.Id.AddCounterpartyNIP);
            AddCounterpartyButton = view.FindViewById<Button>(Resource.Id.AddCounterpartyButton);
          
            AddCounterpartyName.AfterTextChanged += AfterTextChanged;
            AddCounterpartyStreet.AfterTextChanged += AfterTextChanged;
            AddCounterpartyCity.AfterTextChanged += AfterTextChanged;
            AddCounterpartyPostalCode.AfterTextChanged += AfterTextChanged;
            AddCounterpartyPhone.AfterTextChanged += AfterTextChanged;
            AddCounterpartyNIP.AfterTextChanged += AfterTextChanged;

            AddCounterpartyName.OnFocusChangeListener = this;
            AddCounterpartyButton.SetOnClickListener(this);
            
            AddCounterpartyButton.Enabled = false;

            var token = CancelAndSetTokenForView(AddCounterpartyCity);

            Entity = new Models.Counterparty();

            _cityAdapter = new BaseArrayAdapter<Models.City>(Context);
            
            AddCounterpartyCity.Adapter = _cityAdapter;
            AddCounterpartyCity.Threshold = 1;
            Criteria.ItemsPerPage = 10;

            var task = Task.Run(GetCities, token);

            AddCounterpartyCity.ItemClick += OnAutocompleteCityClick;

            return view;
        }

        private void OnAutocompleteCityClick(object adapter, AdapterView.ItemClickEventArgs eventArgs)
        {
            var item = _cityAdapter.GetItem(eventArgs.Position);
            Entity.City.Id = item.Id;
            Entity.City.Name = item.Name;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            Validate();

            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            ViewToObjectMap[editText.Id](Entity, text);

            if (editText.Id == Resource.Id.AddCounterpartyCity)
            {
                var token = CancelAndSetTokenForView(AddCounterpartyCity);

                Criteria.Name = text;

                var task = Task.Run(GetCities, token);
            } 
        }

        public async Task GetCities()
        {
            var result = await CityService.GetCities(Criteria);

            Activity.RunOnUiThread(() =>
            {
                if(result.Error.Any())
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

        public void OnClick(View view)
        {
            var token = CancelAndSetTokenForView(AddCounterpartyButton);
            AddCounterpartyButton.Enabled = false;

            Task.Run(async () =>
            {
                var result = await CounterpartyService.AddCounterparty(Entity, token);

                if (result.Error.Any())
                {
                    RunOnUiThread(() =>
                    {
                        ShowToastMessage(Resource.String.ErrorOccurred);
                        AddCounterpartyButton.Enabled = true;

                    });

                    return;
                }

                NavigationManager.GoToCounterparties();

            }, token);   
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

                    var result = await CounterpartyService.CounterpartyExists(AddCounterpartyName.Text);

                    if(result.Error.Any())
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