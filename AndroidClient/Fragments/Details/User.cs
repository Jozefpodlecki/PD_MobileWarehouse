using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Client.Adapters;

namespace Client.Fragments.Details
{
    public class User : BaseFragment
    {
        public TextView UsereDetailsUsername { get; set; }
        public TextView UsereDetailsEmail { get; set; }
        public TextView UserDetailsRole { get; set; }
        public ListView UserDetailsClaims { get; set; }
        private CheckBoxPermissionsAdapter _adapter;
        public Models.User Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.UserDetails, container, false);

            UsereDetailsUsername = view.FindViewById<TextView>(Resource.Id.UserDetailsUsername);
            UsereDetailsEmail = view.FindViewById<TextView>(Resource.Id.UserDetailsEmail);
            UserDetailsRole = view.FindViewById<TextView>(Resource.Id.UserDetailsRole);
            UserDetailsClaims = view.FindViewById<ListView>(Resource.Id.UserDetailsClaims);

            Entity = (Models.User)Arguments.GetParcelable(Constants.Entity);

            UsereDetailsUsername.Text = Entity.Username;
            UsereDetailsEmail.Text = Entity.Email;
            UserDetailsRole.Text = Entity.Role.ToString();

            Entity.Claims.ForEach(cl => cl.Checked = true);
            _adapter = new CheckBoxPermissionsAdapter(Context, Entity.Claims, false);
            UserDetailsClaims.Adapter = _adapter;

            return view;
        }
    }
}