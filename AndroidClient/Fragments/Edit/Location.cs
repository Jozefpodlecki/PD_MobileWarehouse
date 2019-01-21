using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments.Edit
{
    public class Location : BaseEditFragment<Models.Location>
    {
        public EditText SaveLocationName { get; set; }

        public static Dictionary<int, Action<Models.Location, object>> ViewToObjectMap = new Dictionary<int, Action<Models.Location, object>>()
        {
            { Resource.Id.SaveLocationName, (model, data) => { model.Name = (string)data; } }
        };

        public Location() : base(Resource.Layout.LocationEdit)
        {
        }

        public override void OnBindElements(View view)
        {
            SaveLocationName = view.FindViewById<EditText>(Resource.Id.SaveLocationName);

            SaveLocationName.AfterTextChanged += AfterTextChanged;

            Entity = (Models.Location)Arguments.GetParcelable(Constants.Entity);

            SaveLocationName.Text = Entity.Name;
        }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(SaveLocationName.Text);
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            ValidateRequired(editText);
            ViewToObjectMap[editText.Id](Entity, text);
        }

        public override async Task OnSaveButtonClick(CancellationToken token)
        {
            var result = await LocationService.UpdateLocation(Entity);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    SaveButton.Enabled = true;
                    ShowToastMessage(Resource.String.ErrorOccurred);
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