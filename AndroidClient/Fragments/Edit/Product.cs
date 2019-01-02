using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Listeners;
using Java.Lang;

namespace Client.Fragments.Edit
{
    public class Product : BaseFragment,
        View.IOnClickListener,
        IAfterTextChangedListener,
        IOnItemClickListener,
        IOnBarcodeReadListener
    {
        public ImageView SaveProductImage { get; set; }
        public ImageButton SaveProductImageButton { get; set; }
        public ImageButton SaveProductBarcodeButton { get; set; }
        public ListView SaveProductAttributes { get; set; }
        public ImageButton SaveProductAddAttributeButton { get; set; }
        public Button SaveProductButton { get; set; }
        public Models.Product Entity { get; set; }
        private ProductAttributesEditAdapter _productAttributesEditAdapter;
        private CancellationTokenSource _cancellationTokenSource;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.ProductEdit, container, false);
            SaveProductImage = view.FindViewById<ImageView>(Resource.Id.SaveProductImage);
            SaveProductImageButton = view.FindViewById<ImageButton>(Resource.Id.SaveProductImageButton);
            SaveProductBarcodeButton = view.FindViewById<ImageButton>(Resource.Id.SaveProductBarcodeButton);
            SaveProductAttributes = view.FindViewById<ListView>(Resource.Id.SaveProductAttributes);
            SaveProductAddAttributeButton = view.FindViewById<ImageButton>(Resource.Id.SaveProductAddAttributeButton);
            SaveProductButton = view.FindViewById<Button>(Resource.Id.SaveProductButton);
            SaveProductAddAttributeButton.SetOnClickListener(this);
            SaveProductButton.SetOnClickListener(this);
            SaveProductImageButton.SetOnClickListener(this);
            SaveProductBarcodeButton.SetOnClickListener(this);

            Entity = (Models.Product)Arguments.GetParcelable(Constants.Entity);

            _productAttributesEditAdapter = new ProductAttributesEditAdapter(Context);
            SaveProductAttributes.Adapter = _productAttributesEditAdapter;
            _productAttributesEditAdapter.UpdateList(Entity.ProductAttributes);
            _productAttributesEditAdapter.IOnClickListener = this;
            _productAttributesEditAdapter.IAfterTextChangedListener = this;
            _productAttributesEditAdapter.OnItemClickListener = this;

            SetImage(Entity.Avatar, SaveProductImage);

            if (!_productAttributesEditAdapter.Items.Any())
            {
                _productAttributesEditAdapter.Add(
                new Models.ProductAttribute
                {
                    Attribute = new Models.Attribute()
                });
            }

            return view;
        }

        public void ItemClick(View view, Java.Lang.Object baseObject)
        {
            var productAttribute = (Models.ProductAttribute)view.Tag;
            var attribute = (Models.Attribute)baseObject;
            productAttribute.Attribute = attribute;
        }

        public void AfterTextChanged(EditText view, string text)
        {
            var item = view.Tag as Models.ProductAttribute;

            if(item == null)
            {
                return;
            }

            Criteria.Name = text;

            switch (view.Id)
            {
                case Resource.Id.ProductAttributeEditRowItemName:
                    item.Attribute = new Models.Attribute
                    {
                        Name = text
                    };
                    break;
                case Resource.Id.ProductAttributeEditRowItemValue:
                    item.Value = text;
                    break;
            }

            if (view.Id == Resource.Id.ProductAttributeEditRowItemName)
            {
                if(_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Cancel();
                }

                _cancellationTokenSource = new CancellationTokenSource();
                var token = _cancellationTokenSource.Token;

                Task.Run(async () =>
                {
                    var result = await AttributeService.GetAttributes(Criteria, token);

                    if (result.Error.Any())
                    {
                        RunOnUiThread(() =>
                        {
                            ShowToastMessage(Resource.String.ErrorOccurred);
                        });

                        return;
                    }

                    RunOnUiThread(() =>
                    {
                        _productAttributesEditAdapter
                            .AttributeAdapter
                            .UpdateList(result.Data);
                    });
                }, token);
            }
        }

        public void OnClick(View view)
        {
            if (view.Id == Resource.Id.SaveProductBarcodeButton)
            {
                NavigationManager.GoToBarcodeScanner(true, Entity.Name);
            }
            if (view.Id == Resource.Id.ProductAttributeEditRowItemDelete)
            {
                var item = (Models.ProductAttribute)view.Tag;
                _productAttributesEditAdapter.Remove(item);
            }
            if (view.Id == SaveProductAddAttributeButton.Id)
            {
                _productAttributesEditAdapter
                    .Add(
                    new Models.ProductAttribute
                    {
                        Attribute = new Models.Attribute()
                    });
            }
            if(view.Id == SaveProductImageButton.Id)
            {
                if (!CheckAndRequestPermission(Android.Manifest.Permission.Camera))
                {
                    return;
                }

                CameraProvider.TakePhoto();
            }
            if(view.Id == SaveProductButton.Id)
            {
                SaveProductButton.Enabled = false;
                var token = CancelAndSetTokenForView(view);

                Task.Run(async () =>
                {
                    try
                    {
                        var result = await ProductService.UpdateProduct(Entity, token);

                        if (result.Error.Any())
                        {
                            RunOnUiThread(() =>
                            {
                                SaveProductButton.Enabled = true;
                                ShowToastMessage(Resource.String.ErrorOccurred);
                            });

                            return;
                        }

                        RunOnUiThread(() =>
                        {
                            NavigationManager.GoToProducts();
                        });
                    }
                    catch (System.Exception ex)
                    {

                    }
                    
                });
            }
        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                var bitmap = (Bitmap)data.Extras.Get(Constants.BitmapExtraData);
                SaveProductImage.SetImageBitmap(bitmap);
                ShowToastMessage(Resource.String.CompressingImage);
                SaveProductImageButton.Tag = SaveProductImageButton.Enabled;
                SaveProductImageButton.Enabled = false;

                Task.Run(async () =>
                {
                    var outputStream = new MemoryStream();
                    await bitmap.CompressAsync(Bitmap.CompressFormat.Png, 100, outputStream);
                    var byteArray = outputStream.ToArray();
                    var base64String = Convert.ToBase64String(byteArray);
                    Entity.Avatar = base64String;

                    RunOnUiThread(() =>
                    {
                        ShowToastMessage(Resource.String.CompressingImageComplete);
                        SaveProductImage.Enabled = (bool)SaveProductImage.Tag;
                    });
                });

                return;
            }

            ShowToastMessage(Resource.String.PermissionCameraAborted);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (HasPermission(Android.Manifest.Permission.Camera, permissions, grantResults))
            {
                CameraProvider.TakePhoto();
                return;
            }

            ShowToastMessage(Resource.String.CameraPermission);
        }

        public void OnBarcodeRead(string data)
        {
            Entity.Barcode = data;
        }
    }
}