using Android.Provider;
using Android.App;
using Android.Content;

namespace Client.Providers
{
    public class CameraProvider
    {
        private const int CAMERA_REQUEST = 1888;
        private readonly Activity _activity;

        public CameraProvider(Activity activity)
        {
            _activity = activity;
        }

        public void TakePhoto()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);

            _activity.StartActivityForResult(intent, CAMERA_REQUEST);
        }
    }
}