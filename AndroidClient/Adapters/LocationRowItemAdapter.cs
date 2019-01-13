using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class LocationRowItemAdapter : BaseRecyclerViewAdapter<Models.Location, LocationRowItemViewHolder>
    {
        public ViewStates EditVisibility;
        public ViewStates DeleteVisibility;

        public LocationRowItemAdapter(Context context, IRoleManager roleManager) : base(context, roleManager, Resource.Layout.LocationsRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.LocationRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            EditVisibility = roleManager.Permissions.ContainsKey(Resource.Id.LocationRowItemEdit) ? ViewStates.Visible : ViewStates.Invisible;
        }

        public override void BindItemToViewHolder(Models.Location item, LocationRowItemViewHolder viewHolder)
        {
            viewHolder.LocationRowItemName.Text = item.Name;
            viewHolder.LocationRowItemEdit.SetOnClickListener(IOnClickListener);
            viewHolder.LocationRowItemDelete.SetOnClickListener(IOnClickListener);

            viewHolder.LocationRowItemDelete.Visibility = DeleteVisibility;
            viewHolder.LocationRowItemEdit.Visibility = EditVisibility;
        }

        public override LocationRowItemViewHolder CreateViewHolder(View view) => new LocationRowItemViewHolder(view);

    }
}