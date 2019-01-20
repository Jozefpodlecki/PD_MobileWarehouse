using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Fragments
{
    public class Account : BaseFragment,
        View.IOnClickListener
    {
        public TextView AccountName { get; private set; }
        public TextView AccountRole { get; private set; }
        public ImageView AccountImage { get; private set; }
        public Button ChangeDetailsButton { get; set; }
        public Models.User Entity { get; set; }

        public Account() : base(Resource.Layout.Account)
        {
            
        }

        public override void OnBindElements(View view)
        {
            AccountImage = view.FindViewById<ImageView>(Resource.Id.AccountImage);
            AccountName = view.FindViewById<TextView>(Resource.Id.AccountName);
            AccountRole = view.FindViewById<TextView>(Resource.Id.AccountRole);
            ChangeDetailsButton = view.FindViewById<Button>(Resource.Id.ChangeDetailsButton);

            Task.Run(GetCurrentUser);

            ChangeDetailsButton.SetOnClickListener(this);
        }

        public async Task GetCurrentUser()
        {
            var token = PersistenceProvider.GetToken();
            
            var result = await UserService.GetUser(int.Parse(token.Id));

            if (result.Error.Any())
            {
                RunOnUiThread(() =>
                {
                    ShowToastMessage("An error occurred");
                });
                
                return;
            }

            Entity = result.Data;

            RunOnUiThread(() =>
            {
                if (Entity.Avatar == null)
                {
                    Entity.Avatar = Constants.DefaultBase64PngUserAvatar;
                }

                Helpers.Helpers.Decode64StringAndSetImage(Entity.Avatar, AccountImage);

                AccountName.Text = Entity.FullName;
                AccountRole.Text = Entity.Role.ToString();
            });
        }

        public void OnClick(View view)
        {
            NavigationManager.GoToEditUserProfile(Entity);
        }
    }
}