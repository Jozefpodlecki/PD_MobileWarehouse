using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Services;

namespace Client.Fragments.Add
{
    public class Location : BaseFragment,
        View.IOnClickListener
    {
        public EditText AddLocationName { get; set; }
        public Button AddLocationButton { get; set; }
        public Models.Location Entity { get; set; }

        public static Dictionary<int, Action<Models.Location, object>> ViewToObjectMap = new Dictionary<int, Action<Models.Location, object>>()
        {
            { Resource.Id.AddLocationName, (model, data) => { model.Name = (string)data; } }
        };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddLocation, container, false);
            
            AddLocationName = view.FindViewById<EditText>(Resource.Id.AddLocationName);
            AddLocationButton = view.FindViewById<Button>(Resource.Id.AddLocationButton);

            AddLocationButton.SetOnClickListener(this);
            AddLocationButton.Enabled = false;
            AddLocationName.AfterTextChanged += AfterTextChanged;

            Entity = new Models.Location();

            return view;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            var validated = ValidateRequired(editText);
            AddLocationButton.Enabled = validated;

            ViewToObjectMap[editText.Id](Entity, text);
        }

        public void OnClick(View view)
        {
            var token = CancelAndSetTokenForView(view);
            AddLocationButton.Enabled = false;

            Task.Run(async () =>
            {
                if (!await ValidateLocation(token))
                {
                    RunOnUiThread(() =>
                    {
                        AddLocationButton.Enabled = true;
                    });
                    
                    return;
                }
                
                var result = await LocationService.AddLocation(Entity, token);

                if (result.Error.Any())
                {
                    RunOnUiThread(() =>
                    {
                        ShowToastMessage("An error occurred");
                        AddLocationButton.Enabled = true;
                    });

                    return;
                }

                NavigationManager.GoToLocations();
            }, token);

        }

        private async Task<bool> ValidateLocation(CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            var result = await LocationService.LocationExists(AddLocationName.Text, token);

            token.ThrowIfCancellationRequested();

            if (result.Data)
            {
                RunOnUiThread(() =>
                {
                    SetError(AddLocationName, Resource.String.LocationExists);
                });

                return false;
            }

            RunOnUiThread(() =>
            {
                ClearError(AddLocationName);
            });

            return true;
        }
 
    }
}