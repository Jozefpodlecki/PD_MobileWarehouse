using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Edit
{
    public class Language : BaseFragment,
        View.IOnClickListener
    {      
        public RadioGroup LanguagesRadioGroup { get; set; }
        public Button SaveLanguageButton { get; set; }

        public Language() : base(Resource.Layout.Language)
        {
        }

        public override void OnBindElements(View view)
        {
            SaveLanguageButton = view.FindViewById<Button>(Resource.Id.SaveButton);
            LanguagesRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.LanguagesRadioGroup);

            SaveLanguageButton.SetOnClickListener(this);

            var locale = Context.Resources.Configuration.Locale;

            var radioButtonId = Constants.LanguageResourceMap[locale.ISO3Language];

            var radioButton = LanguagesRadioGroup.FindViewById<RadioButton>(radioButtonId);
            radioButton.Checked = true;
        }

        public void OnClick(View view)
        {
            var token = PersistenceProvider.GetToken();

            var radioButtonId = LanguagesRadioGroup.CheckedRadioButtonId;

            var radioButton = LanguagesRadioGroup.FindViewById<RadioButton>(radioButtonId);
            var iso3Language = (string)radioButton.Tag;

            PersistenceProvider.SetLanguage(iso3Language);
            Activity.SetLanguage(iso3Language);

            Activity.StartActivity(Activity.Intent);
            Activity.Finish();
        }
    }
}