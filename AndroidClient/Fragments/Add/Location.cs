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
    public class Location : BaseAddFragment<Models.Location>
    {
        public EditText AddLocationName { get; set; }

        public static Dictionary<int, Action<Models.Location, object>> ViewToObjectMap = new Dictionary<int, Action<Models.Location, object>>()
        {
            { Resource.Id.AddLocationName, (model, data) => { model.Name = (string)data; } }
        };

        public Location() : base(Resource.Layout.AddLocation)
        {
            Entity = new Models.Location();
        }

        public override void OnBindElements(View view)
        {
            AddLocationName = view.FindViewById<EditText>(Resource.Id.AddLocationName);
            AddLocationName.AfterTextChanged += AfterTextChanged;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            var validated = ValidateRequired(editText);
            AddButton.Enabled = validated;

            ViewToObjectMap[editText.Id](Entity, text);
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

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(AddLocationName.Text);
        }

        public override async Task OnAddButtonClick(CancellationToken token)
        {
            if (!await ValidateLocation(token))
            {
                RunOnUiThread(() =>
                {
                    AddButton.Enabled = true;
                });

                return;
            }

            var result = await LocationService.AddLocation(Entity, token);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    ShowToastMessage(Resource.String.ErrorOccurred);
                    AddButton.Enabled = true;
                });

                return;
            }

            RunOnUiThread(() =>
            {
                NavigationManager.GoToLocations();
            });
        }
    }
}