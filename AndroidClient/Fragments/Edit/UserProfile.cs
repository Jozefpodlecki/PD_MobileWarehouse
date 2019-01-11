using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments.Edit
{
    public class UserProfile : BaseEditFragment<Models.User>,
        ITextWatcher
    {
        public ImageView EditDetailsAvatar { get; set; }
        public ImageButton EditDetailsSetAvatar { get; set; }
        public EditText EditEmail { get; set; }
        public EditText EditPassword { get; set; }
        public EditText EditPasswordConfirm { get; set; }

        public UserProfile() : base(Resource.Layout.UserProfileEdit)
        {
        }

        public override void OnBindElements(View view)
        {
            EditDetailsAvatar = view.FindViewById<ImageView>(Resource.Id.EditDetailsAvatar);
            EditDetailsSetAvatar = view.FindViewById<ImageButton>(Resource.Id.EditDetailsSetAvatar);
            EditEmail = view.FindViewById<EditText>(Resource.Id.EditEmail);
            EditPassword = view.FindViewById<EditText>(Resource.Id.EditPassword);
            EditPasswordConfirm = view.FindViewById<EditText>(Resource.Id.EditPasswordConfirm);

            EditEmail.AddTextChangedListener(this);
            EditPassword.AddTextChangedListener(this);
            EditPasswordConfirm.AddTextChangedListener(this);
            EditDetailsSetAvatar.SetOnClickListener(this);
            EditEmail.Text = Entity.Email;
            EditPasswordConfirm.Text = EditPassword.Text;

            if (Entity.Avatar != null)
            {
                var byteArray = Convert.FromBase64String(Entity.Avatar);
                var bitmap = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);
                EditDetailsAvatar.SetImageBitmap(bitmap);
            }
        }

        public override void OnOtherButtonClick(View view)
        {
            if (!CheckAndRequestPermission(Android.Manifest.Permission.Camera))
            {
                return;
            }

            CameraProvider.TakePhoto();
        }

        public void AfterTextChanged(IEditable s)
        {
            
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if(HasPermission(Android.Manifest.Permission.Camera, permissions, grantResults))
            {
                CameraProvider.TakePhoto();
                return;
            }

            ShowToastMessage(Resource.String.CameraPermission);
        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if(resultCode == Result.Ok)
            {
                var bitmap = (Bitmap)data.Extras.Get(Constants.BitmapExtraData);
                EditDetailsAvatar.SetImageBitmap(bitmap);
                ShowToastMessage(Resource.String.CompressingImage);
                SaveButton.Tag = SaveButton.Enabled;
                SaveButton.Enabled = false;

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
                        SaveButton.Enabled = (bool)SaveButton.Tag;
                    });
                });

                return;
            }

            ShowToastMessage(Resource.String.PermissionCameraAborted);
            
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }

        public override async Task OnSaveButtonClick(CancellationToken token)
        {
            var result = await UserService.UpdateUser(Entity, token);

            if (result.Error.Any())
            {
                ShowToastMessage(Resource.String.ErrorOccurred);

                return;
            }

            RunOnUiThread(() =>
            {
                NavigationManager.GoToAccount();
            });
        }
    }
}