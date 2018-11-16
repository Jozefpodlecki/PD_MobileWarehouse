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

namespace Client.Adapters
{
    public class AddGoodsReceivedNoteEntry
    {
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }

    public class AddGoodsReceivedNoteAdapter : BaseAdapter
    {
        private List<AddGoodsReceivedNoteEntry> _items;
        private Context _context;

        public AddGoodsReceivedNoteAdapter(Context context, List<AddGoodsReceivedNoteEntry> items)
        {
            _context = context;
            _items = items;
        }

        public override Java.Lang.Object GetItem(int position) => position;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            AddGoodsReceivedNoteAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as AddGoodsReceivedNoteAdapterViewHolder;

            if (holder == null)
            {
                
                var inflater = _context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.AddGoodsReceivedNoteRowItem, parent, false);
                holder = new AddGoodsReceivedNoteAdapterViewHolder(parent);
                //replace with your item and your holder items
                //comment back in
                //
                //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
                view.Tag = holder;
            }


            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

        public override int Count => _items.Count;
    }
}