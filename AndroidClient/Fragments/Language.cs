using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments
{
    public class Language : BaseFragment,
        View.IOnClickListener
    {
        public RadioGroup LanguagesRadioGroup { get; set; }
        public Button SaveLanguageButton { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Language, container, false);

            SaveLanguageButton = view.FindViewById<Button>(Resource.Id.SaveLanguageButton);
            LanguagesRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.LanguagesRadioGroup);

            SaveLanguageButton.SetOnClickListener(this);

            var locale = Context.Resources.Configuration.Locale;
            
            var radioButtonId = Constants.LanguageResourceMap[locale.ISO3Language];
            
            var radioButton = LanguagesRadioGroup.FindViewById<RadioButton>(radioButtonId);
            radioButton.Checked = true;

            return view;
        }

        public void OnClick(View view)
        {
            var token = PersistenceProvider.GetToken();

            var radioButtonId = LanguagesRadioGroup.CheckedRadioButtonId;

            var radioButton = LanguagesRadioGroup.FindViewById<RadioButton>(radioButtonId);
            var iso3Language = (string)radioButton.Tag;
            PersistenceProvider.SetLanguage(iso3Language);

            Activity.StartActivity(Activity.Intent);
            Activity.Finish();
        }
    }
}