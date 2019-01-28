using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Edit
{
    public class Attribute : BaseEditFragment<Models.Attribute>
    {
        public EditText AttributeEditName { get; set; }

        public Attribute() : base(Resource.Layout.AttributeEdit)
        {
        }

        public override void OnBindElements(View view)
        {
            AttributeEditName = view.FindViewById<EditText>(Resource.Id.AttributeEditName);
            AttributeEditName.Text = Entity.Name;
            AttributeEditName.AfterTextChanged += AfterTextChanged;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            Entity.Name = editText.Text;
            ValidateRequired(editText);
        }

        public override bool Validate()
        {
            if (string.IsNullOrEmpty(AttributeEditName.Text))
            {
                return false;
            }

            return true;
        }

        public override async Task OnSaveButtonClick(CancellationToken token)
        {
            var result = await AttributeService.EditAttribute(Entity);

            if (result.Error.Any())
            {
                var message = result.Error.FirstOrDefault().Value.FirstOrDefault();
                RunOnUiThread(() =>
                {
                    ShowToastMessage(message);
                });
            }

            RunOnUiThread(() =>
            {
                NavigationManager.GoToAttributes();
            });
        }
    }
}