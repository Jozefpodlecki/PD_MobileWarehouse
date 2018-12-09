using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Client.Helpers;
using Client.Services;
using Java.Lang;

namespace Client.Fragments.Add
{
    public class Location : BaseFragment,
        View.IOnClickListener,
        View.IOnFocusChangeListener,
        ITextWatcher
    {
        public EditText AddLocationName { get; set; }
        public Button AddLocationButton { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddLocation, container, false);

            //var actionBar = Activity.SupportActionBar;
            //actionBar.Title = "Add Location";

            AddLocationName = view.FindViewById<EditText>(Resource.Id.AddLocationName);
            AddLocationButton = view.FindViewById<Button>(Resource.Id.AddLocationButton);

            AddLocationName.OnFocusChangeListener = this;
            AddLocationButton.SetOnClickListener(this);
            AddLocationName.AddTextChangedListener(this);

            return view;
        }

        public async void OnClick(View view)
        {
            var validated = await Validate();

            if (!validated)
            {
                return;
            }

            var location = new Common.DTO.Location
            {
                Name = AddLocationName.Text
            };

            var result = await LocationService.AddLocation(location);

            if (result.Error != null)
            {
                ShowToastMessage("An error occurred");

                return;
            }

            NavigationManager.GoToLocations();
        }

        public async void OnFocusChange(View v, bool hasFocus)
        {
            await Validate();
        }

        private async Task<bool> Validate(CancellationToken token = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(AddLocationName.Text))
            {
                return false;
            }

            var result = await LocationService.LocationExists(AddLocationName.Text, token);
            
            if (result.Data)
            {
                AddLocationName.SetError("Location already exists!", null);
                AddLocationName.RequestFocus();

                return false;
            }

            AddLocationName.SetError((string)null, null);

            return true;
        }

        public void AfterTextChanged(IEditable text)
        {
            var token = CancelAndSetTokenForView(AddLocationName);

            Task.Run(async () =>
            {
                try
                {
                    await Validate(token);
                }
                catch (System.OperationCanceledException exception)
                {
                }
            }, token);
        }
        
    }
}