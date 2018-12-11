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
using Client.Models;
using Client.Services;
using Client.ViewHolders;
using Common.DTO;

namespace Client.Adapters
{
    public class RoleAdapter : BaseRecyclerViewAdapter<Models.Role, RoleViewHolder>
    {
        public RoleAdapter(Context context) : base(context, Resource.Layout.RoleRowItem)
        {
        }

        public override void BindItemToViewHolder(Models.Role item, RoleViewHolder viewHolder)
        {
            viewHolder.RoleRowItemName.Text = item.Name;
            viewHolder.RoleRowItemInfo.SetOnClickListener(IOnClickListener);
            viewHolder.RoleRowItemDelete.SetOnClickListener(IOnClickListener);
            viewHolder.RoleRowItemEdit.SetOnClickListener(IOnClickListener);
        }

        public override RoleViewHolder CreateViewHolder(View view) => new RoleViewHolder(view);
    }
}