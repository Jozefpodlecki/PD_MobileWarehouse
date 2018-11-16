using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common.DTO;
using static Android.Views.View;

namespace Client.Fragments.Add
{
    public class Role : Fragment,
        View.IOnClickListener
    {
        public EditText AddRoleName { get; set; }
        public ListView AddRolePermissionsList { get; set; }
        public Button AddRoleButton { get; set; }
        public new MainActivity Activity => (MainActivity)base.Activity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AddRole, container, false);

            AddRoleName = view.FindViewById<EditText>(Resource.Id.AddRoleName);
            AddRolePermissionsList = view.FindViewById<ListView>(Resource.Id.AddRolePermissionsList);
            AddRoleButton = view.FindViewById<Button>(Resource.Id.AddRoleButton);

            AddRoleButton.SetOnClickListener(this);

            var roleService = new RoleService(Activity);

            List<Claim> items = null;

            var task = Task.Run(async () =>
            {
                items = await roleService.GetClaims();
            });

            task.Wait();

            var adapter = new AddRoleRowItemAdapter(Context, items);

            AddRolePermissionsList.Adapter = adapter;

            AddRoleButton.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View view)
        {
            Activity.NavigationManager.GoToRoles();
        }

        
    }
}