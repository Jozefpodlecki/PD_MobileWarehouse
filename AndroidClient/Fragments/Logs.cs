using Android.OS;
using Android.Views;

namespace Client.Fragments
{
    public class Logs : BaseFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Login, container, false);

            return view;
        }
    }
}