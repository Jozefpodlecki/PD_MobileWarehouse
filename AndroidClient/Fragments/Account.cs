
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments
{
    public class Account : BaseFragment,
        View.IOnClickListener
    {
        public Button ChangeDetailsButton { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Account, container, false);

            ChangeDetailsButton = view.FindViewById<Button>(Resource.Id.ChangeDetailsButton);

            ChangeDetailsButton.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View view)
        {
            NavigationManager.GoToEditDetails();
        }

    }
}