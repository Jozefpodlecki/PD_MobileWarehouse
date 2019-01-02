using Android.Provider;
using Android.App;
using Android.Content;
using System;

namespace Client.Providers
{
    public class CameraProvider
    {
        //private const int CAMERA_REQUEST = 1888;
        private const int CAMERA_REQUEST = 0;
        private readonly Fragment _fragment;

        public CameraProvider(Fragment fragment)
        {
            _fragment = fragment;
        }

        public void TakePhoto()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);

            _fragment.StartActivityForResult(intent, CAMERA_REQUEST);           
        }
    }
}