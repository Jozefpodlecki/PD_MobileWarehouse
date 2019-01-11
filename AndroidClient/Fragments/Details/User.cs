using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Client.Adapters;

namespace Client.Fragments.Details
{
    public class User : BaseDetailsFragment<Models.User>
    {
        public TextView UsereDetailsUsername { get; set; }
        public TextView UsereDetailsEmail { get; set; }
        public TextView UserDetailsRole { get; set; }
        public ListView UserDetailsClaims { get; set; }
        private CheckBoxPermissionsAdapter _adapter;

        public User() : base(Resource.Layout.UserDetails)
        {
        }

        public override void OnBindElements(View view)
        {
            UsereDetailsUsername = view.FindViewById<TextView>(Resource.Id.UserDetailsUsername);
            UsereDetailsEmail = view.FindViewById<TextView>(Resource.Id.UserDetailsEmail);
            UserDetailsRole = view.FindViewById<TextView>(Resource.Id.UserDetailsRole);
            UserDetailsClaims = view.FindViewById<ListView>(Resource.Id.UserDetailsClaims);

            UsereDetailsUsername.Text = Entity.Username;
            UsereDetailsEmail.Text = Entity.Email;
            UserDetailsRole.Text = Entity.Role.ToString();

            Entity.Claims.ForEach(cl => cl.Checked = true);
            _adapter = new CheckBoxPermissionsAdapter(Context, Entity.Claims, false);
            UserDetailsClaims.Adapter = _adapter;
        }
    }
}