
using Android.App;
using Android.OS;
using Android.Views;

namespace Client.Fragments.Details
{
    public class Product : BaseFragment
    {
        public Product()
        {

        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.ProductDetails, container, false);
        }
    }
}