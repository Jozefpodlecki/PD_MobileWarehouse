using Android.OS;
using Android.Views;
using Android.Widget;
using Client.Adapters;

namespace Client.Fragments.Details
{
    public class Product : BaseFragment
    {
        public ImageView ProductDetailsImage { get; set; }
        public TextView ProductDetailsName { get; set; }
        public ListView ProductDetailsAttributes { get; set; }
        public ListView ProductDetailsDetails { get; set; }
        public Models.Product Entity { get; set; }

        private ProductAttributesDetailsAdapter _productAttributesDetailsAdapter;
        private ProductDetailsDetailsAdapter _productDetailsDetailsAdapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.ProductDetails, container, false);

            ProductDetailsImage = view.FindViewById<ImageView>(Resource.Id.ProductDetailsImage);
            ProductDetailsName = view.FindViewById<TextView>(Resource.Id.ProductDetailsName);
            ProductDetailsAttributes = view.FindViewById<ListView>(Resource.Id.ProductDetailsAttributes);
            ProductDetailsDetails = view.FindViewById<ListView>(Resource.Id.ProductDetailsDetails);

            Entity = (Models.Product)Arguments.GetParcelable(Constants.Entity);

            _productAttributesDetailsAdapter = new ProductAttributesDetailsAdapter(Context);
            _productDetailsDetailsAdapter = new ProductDetailsDetailsAdapter(Context);

            ProductDetailsAttributes.Adapter = _productAttributesDetailsAdapter;
            ProductDetailsDetails.Adapter = _productDetailsDetailsAdapter;

            _productAttributesDetailsAdapter.UpdateList(Entity.ProductAttributes);
            _productDetailsDetailsAdapter.UpdateList(Entity.ProductDetails);

            return view;
        }
    }
}