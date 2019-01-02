using System.Linq;
using System.Threading.Tasks;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Add
{
    public class Attribute : BaseFragment,
        View.IOnClickListener
    {
        public EditText AddAttributeName { get; set; }
        public Button AddAttributeButton { get; set; }
        public Models.Attribute Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddAttribute, container, false);
            AddAttributeName = view.FindViewById<EditText>(Resource.Id.AddAttributeName);
            AddAttributeButton = view.FindViewById<Button>(Resource.Id.AddAttributeButton);
            AddAttributeButton.SetOnClickListener(this);
            AddAttributeName.AfterTextChanged += AfterTextChanged;

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
                var result = await AttributeService.AddAttribute(Entity);

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