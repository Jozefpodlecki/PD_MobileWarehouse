using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Models;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class AttributesRowItemAdapter : BaseRecyclerViewAdapter<Models.Attribute, AttributesRowItemViewHolder>
    {
        public ViewStates EditVisibility;
        public ViewStates DeleteVisibility;

        public AttributesRowItemAdapter(
            Context context,
            IRoleManager roleManager) 
            : base(context, roleManager, Resource.Layout.AttributesRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.CounterpartiesRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            EditVisibility = roleManager.Permissions.ContainsKey(Resource.Id.CounterpartiesRowItemEdit) ? ViewStates.Visible : ViewStates.Invisible;
        }

        public override void BindItemToViewHolder(Attribute item, AttributesRowItemViewHolder viewHolder)
        {
            viewHolder.AttributesRowItemName.Text = item.Name;

            viewHolder.AttributesRowItemEdit.SetOnClickListener(IOnClickListener);
            viewHolder.AttributesRowItemDelete.SetOnClickListener(IOnClickListener);
        }

        public override AttributesRowItemViewHolder CreateViewHolder(View view) => new AttributesRowItemViewHolder(view);
    }
}