using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class RoleAdapter : BaseRecyclerViewAdapter<Models.Role, RoleViewHolder>
    {
        public ViewStates EditVisibility;
        public ViewStates DeleteVisibility;
        public ViewStates ReadVisibility;

        public RoleAdapter(Context context, IRoleManager roleManager) : base(context, roleManager, Resource.Layout.RoleRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.RoleRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            ReadVisibility = roleManager.Permissions.ContainsKey(Resource.Id.RoleRowItemInfo) ? ViewStates.Visible : ViewStates.Invisible;
            EditVisibility = roleManager.Permissions.ContainsKey(Resource.Id.RoleRowItemEdit) ? ViewStates.Visible : ViewStates.Invisible;
        }

        public override void BindItemToViewHolder(Models.Role item, RoleViewHolder viewHolder)
        {
            viewHolder.RoleRowItemName.Text = item.Name;
            viewHolder.RoleRowItemInfo.SetOnClickListener(IOnClickListener);
            viewHolder.RoleRowItemDelete.SetOnClickListener(IOnClickListener);
            viewHolder.RoleRowItemEdit.SetOnClickListener(IOnClickListener);

            viewHolder.RoleRowItemDelete.Visibility = DeleteVisibility;
            viewHolder.RoleRowItemEdit.Visibility = EditVisibility;
            viewHolder.RoleRowItemInfo.Visibility = ReadVisibility;
        }

        public override RoleViewHolder CreateViewHolder(View view) => new RoleViewHolder(view);
    }
}