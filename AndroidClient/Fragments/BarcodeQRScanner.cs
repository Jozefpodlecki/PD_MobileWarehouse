using System;
using System.Linq;
using System.Threading;
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
        public BarcodeDetector BarcodeDetector { get; private set; }
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

                if (_callback)
                {
                    _onBarcodeReadListener = NavigationManager.LastFragment as IOnBarcodeReadListener;
                }

#if DEBUG
                _scannedBarcode = Guid.NewGuid().ToString();
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

                        RunOnUiThread(() =>
                        {
                            NavigationManager.GoToProductDetails(result.Data);
                        });

                    }, token);
                }
                else
                {
                    NavigationManager.GoToPrevious();
                }
#else
            var barcodeFormats = Arguments.GetIntArray(Constants.BarcodeFormats)
                .Cast<BarcodeFormat>()
                .Aggregate((acc,barfor) => acc | barfor);

            BarcodeDetector = new BarcodeDetector.Builder(Context)
                .SetBarcodeFormats(barcodeFormats)
                .Build();

            CameraSource = new CameraSource
                .Builder(Context, BarcodeDetector)
                .SetRequestedPreviewSize(640, 480)
                .SetFacing(CameraFacing.Back)
                .Build();

            BarcodeDetector.SetProcessor(this);

            CameraPreview.Holder.AddCallback(this);
            CameraPreviewProgressBar.Visibility = ViewStates.Invisible;
#endif
            }
            catch (System.Exception ex)
            {
                ShowToastMessage(ex.Message, ToastLength.Long);

                NavigationManager.GoToPrevious();
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

        public bool IsParsing = false;
        public Task Task { get; set; }
        public void ReceiveDetections(Detections detections)
        {

            var codes = detections.DetectedItems;

            if(codes.Size() == 0)
            {
                return;
            }

            if(Task != null)
            {
                if (Task.IsCanceled)
                {
                    Task = null;
                    RunOnUiThread(() =>
                    {
                        ShowToastMessage("Task.IsCanceled", ToastLength.Long);
                    });
                    Thread.Sleep(5000);
                    return;
                }
                if (Task.IsFaulted)
                {
                    Task = null;
                    RunOnUiThread(() =>
                    {
                        ShowToastMessage("Task.IsFaulted", ToastLength.Long);
                    });
                    Thread.Sleep(5000);
                    return;
                }
                if (Task.IsCompletedSuccessfully)
                {
                    Task = null;
                    RunOnUiThread(() =>
                    {
                        ShowToastMessage("Task.IsCompletedSuccessfully", ToastLength.Long);
                    });
                    Thread.Sleep(5000);
                    return;
                }
                if (Task.IsCompleted)
                {
                    Task = null;
                    RunOnUiThread(() =>
                    {
                        ShowToastMessage("Task.IsCompleted", ToastLength.Long);
                    });
                    Thread.Sleep(5000);
                    return;
                }

                ShowToastMessage(Task.Status.ToString());
                Thread.Sleep(5000);
                return;
            }

            BarcodeDetector.Release();
            var barcode = (Barcode)codes.ValueAt(0);

            _scannedBarcode = barcode.RawValue;
            CameraPreviewProgressBar.Visibility = ViewStates.Visible;

            RunOnUiThread(() =>
            {
                ShowToastMessage(_scannedBarcode, ToastLength.Long);
            });
            
            Thread.Sleep(5000);
            if (!_callback)
            {
                Task = Task.Run(async () =>
                {
                    try
                    {
                        var result = await ProductService.GetProductByBarcode(_scannedBarcode);

                        if (result.Error.Any())
                        {
                            var message = Resources.GetString(Resource.String.ProductBarcodeNotFound);

                            RunOnUiThread(() =>
                            {
                                BarcodeDetector.SetProcessor(this);
                                ShowToastMessage(message, ToastLength.Long);
                            });

                            return;
                        }

                        RunOnUiThread(() =>
                        {
                            NavigationManager.GoToProductDetails(result.Data);
                        });
                    }
                    catch (Exception ex)
                    {
                        RunOnUiThread(() =>
                        {
                            BarcodeDetector.SetProcessor(this);
                            ShowToastMessage(ex.Message,ToastLength.Long);
                        });
                    }
                    
                });
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
            if(_onBarcodeReadListener != null)
            {
                _onBarcodeReadListener.OnBarcodeRead(_scannedBarcode);
            }

            base.OnDestroy();
        }
    }
}