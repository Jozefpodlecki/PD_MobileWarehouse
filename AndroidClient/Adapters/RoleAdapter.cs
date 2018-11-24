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
using Client.Services;
using Client.ViewHolders;
using Common.DTO;

namespace Client.Adapters
{
    public class RoleAdapter : BaseRecyclerViewAdapter<Role, RoleService, RoleViewHolder>
    {

        public RoleAdapter(Context context, RoleService service, int resourceId)
            : base(context, service, resourceId)
        {
        }

        public override void BindItemToViewHolder(Role dto, RoleViewHolder viewHolder)
        {
            viewHolder.RoleRowItemName.Text = dto.Name;
            viewHolder.RoleRowItemDelete.SetOnClickListener(this);
        }

        public override RecyclerView.ViewHolder GetViewHolder(View view)
        {
            return new RoleViewHolder(view);
        }

        public async override void OnClick(View view)
        {
            var holder = view.Tag as RoleViewHolder;
            var position = holder.AdapterPosition;
            var item = _items[position];

            await _service.DeleteRole(item.Id);

            _items.RemoveAt(position);
            NotifyItemRemoved(position);
        }
    }
}