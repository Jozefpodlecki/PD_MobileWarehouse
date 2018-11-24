using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using Client;
using Client.Fragments;
using Client.Services;
using Common.DTO;
using static Android.Views.View;

namespace AndroidClient.Fragments
{
    public class AddProduct : BaseFragment,
        IOnClickListener
    {
        public Dictionary<View, Action> ViewActionMap {get;set;}
        public TableLayout Attributes { get; set; }
        public TableLayout Details { get; set; }
        public TextView AddDetailsTextView { get; set; }
        public TextView AddAttributeTextView { get; set; }
        public Button AddProductButton { get; set; }
        public ImageView AddProductImage { get; set; }
        public EditText AddProductName { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddProduct, container, false);

            Attributes = view.FindViewById<TableLayout>(Resource.Id.AttributesContainer);
            Details = view.FindViewById<TableLayout>(Resource.Id.DetailsContainer);
            AddAttributeTextView = view.FindViewById<TextView>(Resource.Id.AddAttributeTextView);
            AddDetailsTextView = view.FindViewById<TextView>(Resource.Id.AddDetailsTextView);
            AddProductButton = view.FindViewById<Button>(Resource.Id.AddProductButton);
            AddProductImage = view.FindViewById<ImageView>(Resource.Id.AddProductImage);
            AddProductName = view.FindViewById<EditText>(Resource.Id.AddProductName);

            AddAttributeTextView.SetOnClickListener(this);
            AddDetailsTextView.SetOnClickListener(this);
            AddProductButton.SetOnClickListener(this);
            AddProductImage.SetOnClickListener(this);

            ViewActionMap = new Dictionary<View, Action>();
            ViewActionMap[AddProductImage] = OnAddProductImage;
            ViewActionMap[AddDetailsTextView] = OnAddDetailsClick;
            ViewActionMap[AddAttributeTextView] = OnAddAttributeClick;
            ViewActionMap[AddProductButton] = OnAddProductButtonClick;

            return view;
        }

        public void OnClick(View view)
        {
            ViewActionMap[view].Invoke();
        }

        public void OnAddProductImage()
        {

        }

        public async void OnAddProductButtonClick()
        {
            var productService = new ProductService(Activity);

            var productAttributes = new ProductAttribute
            {

            };
            
            var product = new Product
            {
                Name = AddProductName.Text,
                Image = null,
                LastModification = DateTime.Now,
                
                
            };

            await productService.AddProduct(product);
        }

        public void OnAddDetailsClick()
        {
            var tableRow = new TableRow(Activity);
            tableRow.LayoutParameters = new TableRow.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

            //var detailsLayout = LayoutInflater
            //    .From(Activity)
            //    .Inflate(Resource.Layout.AddProductDetails,tableRow);

            //tableRow.AddView(detailsLayout);
        }

        public void OnAddAttributeClick()
        {

        }
    }
}