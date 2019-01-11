using Android.OS;
using Android.Views;
using Android.Widget;
using Client.Adapters;

namespace Client.Fragments.Details
{
    public class Product : BaseDetailsFragment<Models.Product>
    {
        public ImageView ProductDetailsImage { get; set; }
        public TextView ProductDetailsName { get; set; }
        public ListView ProductDetailsAttributes { get; set; }
        public ListView ProductDetailsDetails { get; set; }

        private ProductAttributesDetailsAdapter _productAttributesDetailsAdapter;
        private ProductDetailsDetailsAdapter _productDetailsDetailsAdapter;

        public Product() : base(Resource.Layout.ProductDetails)
        {
        }

        public override void OnBindElements(View view)
        {
            ProductDetailsImage = view.FindViewById<ImageView>(Resource.Id.ProductDetailsImage);
            ProductDetailsName = view.FindViewById<TextView>(Resource.Id.ProductDetailsName);
            ProductDetailsAttributes = view.FindViewById<ListView>(Resource.Id.ProductDetailsAttributes);
            ProductDetailsDetails = view.FindViewById<ListView>(Resource.Id.ProductDetailsDetails);

            _productAttributesDetailsAdapter = new ProductAttributesDetailsAdapter(Context);
            _productDetailsDetailsAdapter = new ProductDetailsDetailsAdapter(Context);

            ProductDetailsAttributes.Adapter = _productAttributesDetailsAdapter;
            ProductDetailsDetails.Adapter = _productDetailsDetailsAdapter;

            _productAttributesDetailsAdapter.UpdateList(Entity.ProductAttributes);
            _productDetailsDetailsAdapter.UpdateList(Entity.ProductDetails);
        }
    }
}