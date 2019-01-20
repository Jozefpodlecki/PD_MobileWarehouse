using Android.Content;
using Android.Graphics;
using Android.Views;
using Client.Managers;
using Client.Managers.Interfaces;
using Client.Providers;
using Client.ViewHolders;
using System;

namespace Client.Adapters
{
    public class CounterpartiesRowItemAdapter : BaseRecyclerViewAdapter<Models.Counterparty, CounterpartiesRowItemViewHolder>
    {
        public ViewStates EditVisibility;
        public ViewStates DeleteVisibility;
        public ViewStates ReadVisibility;
        public ColorConditionalStyleProvider _styleProvider;

        public CounterpartiesRowItemAdapter(
            Context context,
            IRoleManager roleManager) 
            : base(context, roleManager, Resource.Layout.CounterpartiesRowItem)
        {
            DeleteVisibility = roleManager.Permissions.ContainsKey(Resource.Id.CounterpartiesRowItemDelete) ? ViewStates.Visible : ViewStates.Invisible;
            ReadVisibility = roleManager.Permissions.ContainsKey(Resource.Id.CounterpartiesRowItemInfo) ? ViewStates.Visible : ViewStates.Invisible;
            EditVisibility = roleManager.Permissions.ContainsKey(Resource.Id.CounterpartiesRowItemEdit) ? ViewStates.Visible : ViewStates.Invisible;
            _styleProvider = ColorConditionalStyleProvider.Instance;
        }

        public override void BindItemToViewHolder(Models.Counterparty item, CounterpartiesRowItemViewHolder viewHolder)
        {
            viewHolder.CounterpartiesRowItemName.Text = item.Name;
            viewHolder.CounterpartiesRowItemNIP.Text = item.NIP;

            _styleProvider
                .Execute(item.LastModifiedAt,
                viewHolder.CounterpartiesRowItemName,
                viewHolder.CounterpartiesRowItemNIP);

            viewHolder.CounterpartiesRowItemInfo.SetOnClickListener(IOnClickListener);
            viewHolder.CounterpartiesRowItemEdit.SetOnClickListener(IOnClickListener);
            viewHolder.CounterpartiesRowItemDelete.SetOnClickListener(IOnClickListener);
        }

        public override CounterpartiesRowItemViewHolder CreateViewHolder(View view) => new CounterpartiesRowItemViewHolder(view);
    }
}