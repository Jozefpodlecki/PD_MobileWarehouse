using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Helpers;
using Client.Services;
using Common;
using Java.Lang;

namespace Client.Fragments
{
    public class Locations : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddLocationFloatActionButton { get; set; }
        public AutoCompleteTextView SearchLocation { get; set; }
        public RecyclerView LocationsList { get; set; }
        public TextView EmptyLocationView { get; set; }
        private LocationService _service;
        public LocationRowItemAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Locations, container, false);
            
            AddLocationFloatActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddLocationFloatActionButton);
            SearchLocation = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchLocation);
            LocationsList = view.FindViewById<RecyclerView>(Resource.Id.LocationsList);
            EmptyLocationView = view.FindViewById<TextView>(Resource.Id.EmptyLocationView);

            AddLocationFloatActionButton.SetOnClickListener(this);
            SearchLocation.AddTextChangedListener(this);

            _service = new LocationService(Activity);

            _adapter = new LocationRowItemAdapter(Activity, _service);

            var linearLayoutManager = new LinearLayoutManager(Activity)
            {
                Orientation = LinearLayoutManager.Vertical
            };
            LocationsList.SetLayoutManager(linearLayoutManager);

            LocationsList.SetAdapter(_adapter);

            GetLocations();

            return view;
        }

        private void GetLocations()
        {
            var token = CancelAndSetTokenForView(LocationsList);

            var task = Task.Run(async () =>
            {
                var items = await _service.GetLocations(Criteria, token);

                if (items.Error != null)
                {
                    ShowToastMessage("An error occurred");

                    return;
                }

                _adapter.UpdateList(items.Data);

                if (items.Data.Any())
                {
                    EmptyLocationView.Visibility = ViewStates.Invisible;
                    LocationsList.Visibility = ViewStates.Visible;

                    return;
                }

                EmptyLocationView.Visibility = ViewStates.Visible;
                LocationsList.Visibility = ViewStates.Invisible;
                
            }, token);


        }

        public void OnClick(View view)
        {
            NavigationManager.GoToAddLocation();
        }

        public void AfterTextChanged(IEditable text)
        {
            Criteria.Name = text.ToString();

            GetLocations();
        }
        
    }
}