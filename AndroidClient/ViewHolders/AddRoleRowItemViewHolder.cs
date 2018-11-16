using Android.Views;
using Android.Widget;

namespace Client.ViewHolders
{
    public class AddRoleRowItemViewHolder : Java.Lang.Object
    {
        public CheckBox Permission { get; set; }

        public AddRoleRowItemViewHolder(View itemView)
        {
            Permission = itemView.FindViewById<CheckBox>(Resource.Id.AddRoleRowItemPermission);
        }

    }
}