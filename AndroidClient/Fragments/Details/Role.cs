
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Client.Adapters;

namespace Client.Fragments.Details
{
    public class Role : BaseDetailsFragment<Models.Role>
    {
        public TextView RoleDetailsName { get; set; }
        public ListView RoleDetailsClaims { get; set; }
        private CheckBoxPermissionsAdapter _adapter;

        public Role() : base(Resource.Layout.RoleDetails)
        {
        }

        public override void OnBindElements(View view)
        {
            RoleDetailsName = view.FindViewById<TextView>(Resource.Id.RoleDetailsName);
            RoleDetailsClaims = view.FindViewById<ListView>(Resource.Id.RoleDetailsClaims);
            
            RoleDetailsName.Text = Entity.Name;
            Entity.Claims.ForEach(cl => cl.Checked = true);
            _adapter = new CheckBoxPermissionsAdapter(Context, Entity.Claims, false);
            RoleDetailsClaims.Adapter = _adapter;
        }
    }
}