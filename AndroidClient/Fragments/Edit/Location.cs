using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Fragments.Edit
{
    public class Location : BaseFragment,
        View.IOnClickListener
    {
        public Button SaveLocationButton { get; set; }
        public EditText SaveLocationName { get; set; }
        public Models.Location Entity { get; set; }

        public static Dictionary<int, Action<Models.Location, object>> ViewToObjectMap = new Dictionary<int, Action<Models.Location, object>>()
        {
            { Resource.Id.SaveLocationName, (model, data) => { model.Name = (string)data; } }
        };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.LocationEdit, container, false);
            SaveLocationButton = view.FindViewById<Button>(Resource.Id.SaveLocationButton);
            SaveLocationName = view.FindViewById<EditText>(Resource.Id.SaveLocationName);

            SaveLocationButton.SetOnClickListener(this);
            SaveLocationName.AfterTextChanged += AfterTextChanged;

            Entity = (Models.Location)Arguments.GetParcelable(Constants.Entity);

            SaveLocationName.Text = Entity.Name;

            return view;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            var text = eventArgs.Editable.ToString();

            ValidateRequired(editText);
            ViewToObjectMap[editText.Id](Entity, text);
        }

        public void OnClick(View view)
        {
            SaveLocationButton.Enabled = false;

            Task.Run(async () =>
            {
                var result = await LocationService.UpdateLocation(Entity);

                if (result.Error.Any())
                {
                    RunOnUiThread(() => 
                    {
                        SaveLocationButton.Enabled = true;
                        ShowToastMessage("An error occurred");
                    });
                    
                    return;
                }

                RunOnUiThread(() =>
                {
                    NavigationManager.GoToLocations();
                });
            });
            
        }
    }
}