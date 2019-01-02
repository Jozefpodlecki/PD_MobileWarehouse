using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.ViewHolders;
using static Android.Widget.CompoundButton;

namespace Client.Adapters
{
    public class CheckBoxPermissionsAdapter : ArrayAdapter<Models.Claim>
    {
        protected LayoutInflater _layoutInflater { get; set; }
        protected int _resourceId;
        private bool _enabledItems;
        public View.IOnClickListener IOnClickListener { get; set; }
        public IOnCheckedChangeListener IOnCheckedChangeListener { get; set; }

        public CheckBoxPermissionsAdapter(
            Context context,
            List<Models.Claim> objects,
            bool enabledItems = true)
            : base(context, Resource.Layout.CheckBoxRowItem, objects)
        {
            _enabledItems = enabledItems;
            _resourceId = Resource.Layout.CheckBoxRowItem;
            _layoutInflater = LayoutInflater.From(Context);
        }

        public string GetString(string identifierName)
        {
            var packageName = Context.PackageName;
            
            var resourceId = Context.Resources.GetIdentifier(identifierName, "string", packageName);

            if(resourceId == 0)
            {
                return identifierName;
            }

            return Context.GetString(resourceId);
        }

        public List<Models.Claim> SelectedItems => Enumerable
                .Range(0, Count - 1)
                .Select(GetItem)
                .Where(cl => cl.Checked)
                .ToList();

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            CheckBoxRowItemViewHolder holder = null;
            var item = GetItem(position);

            if (convertView == null)
            {              
                convertView = _layoutInflater.Inflate(_resourceId, parent, false);
                holder = new CheckBoxRowItemViewHolder(convertView);
            }
            else
            {
                holder = (CheckBoxRowItemViewHolder)convertView.Tag;
                holder.Permission.SetOnCheckedChangeListener(null);
                convertView.SetOnClickListener(null);
            }

            holder.Permission.Text = GetString(item.Value);
            holder.Permission.Checked = item.Checked;
            holder.Permission.Enabled = _enabledItems;
            holder.Position = position;
            holder.Permission.SetOnCheckedChangeListener(IOnCheckedChangeListener);
            convertView.SetOnClickListener(IOnClickListener);

            holder.Permission.Tag = holder;
            convertView.Tag = holder;

            return convertView;
        }
    }
}