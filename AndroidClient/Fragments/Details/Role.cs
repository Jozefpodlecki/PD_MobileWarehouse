
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Client.Adapters;

namespace Client.Fragments.Details
{
    public class Role : BaseFragment
    {
        public TextView RoleDetailsName { get; set; }
        public ListView RoleDetailsClaims { get; set; }
        private CheckBoxPermissionsAdapter _adapter;
        public Models.Role Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.RoleDetails, container, false);

            RoleDetailsName = view.FindViewById<TextView>(Resource.Id.RoleDetailsName);
            RoleDetailsClaims = view.FindViewById<ListView>(Resource.Id.RoleDetailsClaims);

            Entity = (Models.Role)Arguments.GetParcelable(Constants.Entity);

            RoleDetailsName.Text = Entity.Name;
            Entity.Claims.ForEach(cl => cl.Checked = true);
            _adapter = new CheckBoxPermissionsAdapter(Context, Entity.Claims, false);
            RoleDetailsClaims.Adapter = _adapter;

            return view;
        }
    }
}