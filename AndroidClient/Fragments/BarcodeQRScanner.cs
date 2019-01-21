using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Vision;
using Android.Gms.Vision.Barcodes;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Listeners;
using static Android.Gms.Vision.Detector;
using static Android.Hardware.Camera;
using static Android.Widget.AdapterView;

namespace Client.Fragments
{
    public class BarcodeQRScanner : 
        BaseFragment,
        ISurfaceHolderCallback,
        IProcessor
    {
        public SurfaceView CameraPreview { get; set; }
        public ProgressBar CameraPreviewProgressBar { get; set; }
        public CameraSource CameraSource { get; set; }
        public Vibrator Vibrator { get; set; }
        private bool _callback;
        private IOnBarcodeReadListener _onBarcodeReadListener;
        private string _scannedBarcode;

        public BarcodeQRScanner() : base(Resource.Layout.BarcodeQRScanner)
        {
        }

        public override void OnBindElements(View view)
        {
            try
            {
                CameraPreview = view.FindViewById<SurfaceView>(Resource.Id.CameraPreview);
                CameraPreviewProgressBar = view.FindViewById<ProgressBar>(Resource.Id.CameraPreviewProgressBar);
                Vibrator = (Vibrator)Activity.GetSystemService(Context.VibratorService);
                _callback = Arguments.GetBoolean(Constants.Callback);
                _onBarcodeReadListener = NavigationManager.LastFragment as IOnBarcodeReadListener;

#if DEBUG
                _scannedBarcode = Arguments.GetString("Barcode");
                var token = CancelAndSetTokenForView(CameraPreview);
                if (!_callback)
                {
                    Task.Run(async () =>
                    {
                        var result = await ProductService.GetProductByBarcode(_scannedBarcode, token);

                        if (result.Error.Any())
                        {
                            var message = Resources.GetString(Resource.String.ProductBarcodeNotFound);
                            ShowToastMessage(message);

                            return;
                        }

                        NavigationManager.GoToProductDetails(result.Data);
                    }, token);
                }
                else
                {
                    NavigationManager.GoToPrevious();
                }
#endif
#if RELEASE
            var barcodeFormats = Arguments.GetIntArray(Constants.BarcodeFormats)
                .Cast<BarcodeFormat>()
                .Aggregate((acc,barfor) => acc | barfor);

            var detector = new BarcodeDetector.Builder(Context)
                .SetBarcodeFormats(barcodeFormats)
                .Build();

            CameraSource = new CameraSource
                .Builder(Context, detector)
                .SetRequestedPreviewSize(640, 480)
                .SetFacing(CameraFacing.Back)
                .Build();

            detector.SetProcessor(this);

            CameraPreview.Holder.AddCallback(this);
            CameraPreviewProgressBar.Visibility = ViewStates.Invisible;
#endif
            }
            catch (System.Exception ex)
            {
                ShowToastMessage(ex.Message, ToastLength.Long);
            }

            
        }

        public bool CameraAvailable => Enumerable
                .Range(0, NumberOfCameras - 1)
                .Select((index, val) =>
                {
                    var cameraInfo = new CameraInfo();
                    GetCameraInfo(index, cameraInfo);

                    return cameraInfo;
                })
                .Any(camInf => camInf.Facing == Android.Hardware.CameraFacing.Back);

        public void ReceiveDetections(Detections detections)
        {
            var codes = detections.DetectedItems;

            if(codes.Size() == 0)
            {
                return;
            }

            var token = CancelAndSetTokenForView(CameraPreview);

            var barcode = (Barcode)codes.ValueAt(0);

            _scannedBarcode = barcode.RawValue;
            CameraPreviewProgressBar.Visibility = ViewStates.Visible;
            Vibrator.Vibrate(VibrationEffect.CreateOneShot(1000, 1));

            if (!_callback)
            {
                Task.Run(async () =>
                {
                    var result = await ProductService.GetProductByBarcode(_scannedBarcode, token);

                    if (result.Error.Any())
                    {
                        var message = Resources.GetString(Resource.String.ProductBarcodeNotFound);
                        ShowToastMessage(message);

                        return;
                    }

                    NavigationManager.GoToProductDetails(result.Data);
                }, token);
            }
            else
            {
                NavigationManager.GoToPrevious();
            }
        }

        public void Release()
        {
            
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            if (!CheckAndRequestPermission(Android.Manifest.Permission.Camera))
            {
                return;
            }

            if (CameraAvailable)
            {
                CameraSource.Start(CameraPreview.Holder);

                return;
            }

            ShowToastMessage(Resource.String.ErrorOccurred,ToastLength.Long);
            NavigationManager.GoToPrevious();
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (HasPermission(Android.Manifest.Permission.Camera, permissions, grantResults))
            {
                CameraSource.Start(CameraPreview.Holder);
                return;
            }

            var message = Resources.GetString(Resource.String.CameraPermission);
            ShowToastMessage(message);
        }

        public override void OnDestroy()
        {
            if(_onBarcodeReadListener != null && _callback)
            {
                _onBarcodeReadListener.OnBarcodeRead(_scannedBarcode);
            }

            base.OnDestroy();
        }
    }
}