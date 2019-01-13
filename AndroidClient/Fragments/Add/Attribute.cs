using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Add
{
    public class Attribute : BaseAddFragment<Models.Attribute>
    {
        public EditText AddAttributeName { get; set; }

        public Attribute() : base(Resource.Layout.AddAttribute)
        {
            Entity = new Models.Attribute();
        }

        public override void OnBindElements(View view)
        {
            AddAttributeName = view.FindViewById<EditText>(Resource.Id.AddAttributeName);
            AddAttributeName.AfterTextChanged += AfterTextChanged;
        }
        
        private void AfterTextChanged(object sender, AfterTextChangedEventArgs eventArgs)
        {
            var editText = (EditText)sender;
            Entity.Name = editText.Text;
            ValidateRequired(editText);
        }

        public override bool Validate()
        {
            return ValidateRequired(AddAttributeName);
        }

        public override async Task OnAddButtonClick(CancellationToken token)
        {
            var result = await AttributeService.AddAttribute(Entity);

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    AddButton.Enabled = true;
                });

                return;
            }

            RunOnUiThread(() =>
            {
                NavigationManager.GoToAttributes();
            });
        }
    }
}