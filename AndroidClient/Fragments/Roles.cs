using System;

using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Client.Fragments
{
    public class Roles : Fragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddRoleFloatActionButton { get; set; }
        public AutoCompleteTextView SearchRoles { get; set; }
        public RecyclerView RolesList { get; set; }
        public new MainActivity Activity => (MainActivity)base.Activity;

        public void AfterTextChanged(IEditable s)
        {
            throw new NotImplementedException();
        }
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Roles, container, false);

            AddRoleFloatActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddRoleFloatActionButton);
            SearchRoles = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchRoles);
            RolesList = view.FindViewById<RecyclerView>(Resource.Id.RolesList);

            return view;
        }

        public void OnClick(View view)
        {
            Activity.NavigationManager.GoToAddRole();
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count) { }
        public void BeforeTextChanged(ICharSequence s, int start, int count, int after) { }
    }
}