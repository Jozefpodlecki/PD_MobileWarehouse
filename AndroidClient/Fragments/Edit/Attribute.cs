using System;
using System.Linq;
using System.Threading.Tasks;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Edit
{
    public class Attribute : BaseFragment,
        View.IOnClickListener
    {
        public EditText AttributeEditName { get; set; }
        public Button SaveAttributeButton { get; set; }
        public Models.Attribute Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AttributeEdit, container, false);
            AttributeEditName = view.FindViewById<EditText>(Resource.Id.AttributeEditName);
            SaveAttributeButton = view.FindViewById<Button>(Resource.Id.SaveAttributeButton);
            SaveAttributeButton.SetOnClickListener(this);
            AttributeEditName.AfterTextChanged += AfterTextChanged;

            return view;
        }
        
        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            Entity.Name = editText.Text;
            ValidateRequired(editText);
        }

        public void OnClick(View view)
        {
            Task.Run(async () =>
            {
                var result = await AttributeService.EditAttribute(Entity);

                if (result.Error.Any())
                {

                }

                RunOnUiThread(() =>
                {
                    NavigationManager.GoToAttributes();
                });
            });
        }
    }
}