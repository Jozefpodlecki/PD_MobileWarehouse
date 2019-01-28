using Android.Content;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Models;
using Client.Providers;
using Client.ViewHolders;

namespace Client.Adapters
{
    public class AttributesRowItemAdapter : BaseRecyclerViewAdapter<Models.Attribute, AttributesRowItemViewHolder>
    {
        public ViewStates EditVisibility;
        public ViewStates DeleteVisibility;
        public ColorConditionalStyleProvider _styleProvider;

        public AttributesRowItemAdapter(
            Context context,
            IRoleManager roleManager) 
            : base(context, roleManager, Resource.Layout.AttributesRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.AttributesRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            EditVisibility = roleManager.Permissions.ContainsKey(Resource.Id.AttributesRowItemEdit) ? ViewStates.Visible : ViewStates.Invisible;
            _styleProvider = ColorConditionalStyleProvider.Instance;
        }

        public override void BindItemToViewHolder(Attribute item, AttributesRowItemViewHolder viewHolder)
        {
            viewHolder.AttributesRowItemName.Text = item.Name;

            _styleProvider
                .Execute(item.LastModifiedAt,
                viewHolder.AttributesRowItemName);

            viewHolder.AttributesRowItemEdit.SetOnClickListener(IOnClickListener);
            viewHolder.AttributesRowItemDelete.SetOnClickListener(IOnClickListener);

            viewHolder.AttributesRowItemEdit.Visibility = EditVisibility;
            viewHolder.AttributesRowItemDelete.Visibility = DeleteVisibility;
        }

        public override AttributesRowItemViewHolder CreateViewHolder(View view) => new AttributesRowItemViewHolder(view);
    }
}