
using Android.App;
using Android.OS;
using Android.Views;

namespace Client.Fragments.Details
{
    public class Role : Fragment
    {
        private Common.DTO.Role _role;

        public Role(Common.DTO.Role role)
        {
            _role = role;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.RoleDetails, container, false);



            return view;
        }
    }
}