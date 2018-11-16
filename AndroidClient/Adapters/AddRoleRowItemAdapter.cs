using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.ViewHolders;
using Common.DTO;

namespace Client.Adapters
{
    public class AddItemRole
    {
        public string Name { get; set; }
        public bool Checked { get; set; }
    }

    public class AddRoleRowItemAdapter : BaseAdapter
    {
        private List<Claim> _items;
        private Context _context;

        public AddRoleRowItemAdapter(Context context, List<Claim> items)
        {
            _context = context;
            _items = items;
        }

        public override Java.Lang.Object GetItem(int position) => position;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            AddRoleRowItemViewHolder holder = null;
            var item = _items[position];

            if (view != null)
                holder = view.Tag as AddRoleRowItemViewHolder;

            if (holder == null)
            {

                var inflater = _context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.AddRoleRowItem, parent, false);
                holder = new AddRoleRowItemViewHolder(view);

                view.Tag = holder;
            }

            holder.Permission.Text = item.Value;
            holder.Permission.Checked = false;

            return view;
        }

        public override int Count => _items.Count;
    }
}