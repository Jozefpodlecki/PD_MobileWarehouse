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
using System.Threading.Tasks;

namespace Client.Fragments.Edit
{
    public class UserProfile : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public ImageView EditDetailsAvatar { get; set; }
        public ImageButton EditDetailsSetAvatar { get; set; }
        public EditText EditEmail { get; set; }
        public EditText EditPassword { get; set; }
        public EditText EditPasswordConfirm { get; set; }
        public Button EditDetailsSaveButton { get; set; }
        private string _defaultPassword = "******";
        public Models.User Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.UserProfileEdit, container, false);

            EditDetailsAvatar = view.FindViewById<ImageView>(Resource.Id.EditDetailsAvatar);
            EditDetailsSetAvatar = view.FindViewById<ImageButton>(Resource.Id.EditDetailsSetAvatar);
            EditEmail = view.FindViewById<EditText>(Resource.Id.EditEmail);
            EditPassword = view.FindViewById<EditText>(Resource.Id.EditPassword);
            EditPasswordConfirm = view.FindViewById<EditText>(Resource.Id.EditPasswordConfirm);
            EditDetailsSaveButton = view.FindViewById<Button>(Resource.Id.EditDetailsSaveButton);

            EditEmail.AddTextChangedListener(this);
            EditPassword.AddTextChangedListener(this);
            EditPasswordConfirm.AddTextChangedListener(this);
            EditDetailsSetAvatar.SetOnClickListener(this);
            EditDetailsSaveButton.SetOnClickListener(this);

            Entity = (Models.User)Arguments.GetParcelable(Constants.Entity);

            EditEmail.Text = Entity.Email;
            EditPassword.Text = _defaultPassword;
            EditPasswordConfirm.Text = EditPassword.Text;

            if (Entity.Avatar != null)
            {
                var byteArray = Convert.FromBase64String(Entity.Avatar);
                var bitmap = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);
                EditDetailsAvatar.SetImageBitmap(bitmap);
            }

            return view;
        }

        public void OnClick(View view)
        {
            if(view.Id == EditDetailsSaveButton.Id)
            {
                var token = CancelAndSetTokenForView(EditDetailsSaveButton);

                Entity.Email = EditEmail.Text;

                if(EditPassword.Text != _defaultPassword
                    && !string.IsNullOrEmpty(EditPassword.Text))
                {
                    Entity.Password = EditPassword.Text;
                }

                Task.Run(async () =>
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
                });

            }
            if(view == EditDetailsSetAvatar)
            {
                if (!CheckAndRequestPermission(Android.Manifest.Permission.Camera))
                {
                    return;
                }

                CameraProvider.TakePhoto();
            }
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
                EditDetailsSaveButton.Tag = EditDetailsSaveButton.Enabled;
                EditDetailsSaveButton.Enabled = false;

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
                        EditDetailsSaveButton.Enabled = (bool)EditDetailsSaveButton.Tag;
                    });
                });

                return;
            }

            ShowToastMessage(Resource.String.PermissionCameraAborted);
            
        }
    }
}