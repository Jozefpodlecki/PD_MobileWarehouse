using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.ViewHolders;
using static Android.Widget.CompoundButton;

namespace Client.Adapters
{

    public class AddUserPermissionsAdapter : ArrayAdapter<Models.Claim>,
        IOnCheckedChangeListener,
        View.IOnClickListener
    {
        public List<Models.Claim> Items { get; set; }

        public AddUserPermissionsAdapter(Context context, List<Models.Claim> objects) 
            : base(context, Resource.Layout.AddRoleRowItem, objects)
        {
            Items = objects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            AddRoleRowItemViewHolder holder = null;
            var item = Items[position];

            if (view != null)
                holder = view.Tag as AddRoleRowItemViewHolder;

            if (holder == null)
            {

                var inflater = Context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.AddRoleRowItem, parent, false);
                holder = new AddRoleRowItemViewHolder(view);

                holder.Permission.SetOnCheckedChangeListener(this);
                view.SetOnClickListener(this);

                view.Tag = holder;
            }

            holder.Permission.Tag = position;
            holder.Permission.Text = item.Value;
            holder.Permission.Checked = item.Checked;

            return view;
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            var position = (int)buttonView.Tag;
            Items[position].Checked = isChecked;
        }

        public void OnClick(View view)
        {
            var holder = view.Tag as AddRoleRowItemViewHolder;
            holder.Permission.Checked = !holder.Permission.Checked;
        }
    }
}