using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Details
{
    public class User : BaseFragment
    {
        public RecyclerView Roles { get; set; }
        public RecyclerView Claims { get; set; }
        private Common.DTO.User _user { get; set; }

        public User()
        {

        }

        public User(Common.DTO.User user)
        {
            _user = user;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.UserDetails, container, false);

            //Roles = view.FindViewById<RecyclerView>(Resource.Id.UserDetailsRolesList);
            //Claims = view.FindViewById<RecyclerView>(Resource.Id.UserDetailsClaimsList);

            return view;
        }
    }
}