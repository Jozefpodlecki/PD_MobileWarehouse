using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Common.DTO;
using System.Collections.Generic;

namespace Client.Adapters
{
    //public class CounterpartyAutoCompleteAdapter : ArrayAdapter<Counterparty>
    //{
    //    private LayoutInflater _layoutInflater;
    //    public List<Counterparty> Items;
    //    private int _resourceId;

    //    public CounterpartyAutoCompleteAdapter(Context context)
    //        : base(context, Resource.Layout.CounterpartyAutoCompleteRowItem)
    //    {
    //        _layoutInflater = ((Activity)context).LayoutInflater;
    //        _resourceId = Resource.Layout.CounterpartyAutoCompleteRowItem;
    //        Items = new List<Counterparty>();
    //    }

    //    public void UpdateList(List<Counterparty> items)
    //    {
    //        Items = items;
    //        NotifyDataSetChanged();
    //    }

    //    public override View GetView(int position, View convertView, ViewGroup parent)
    //    {
    //        if(convertView == null)
    //        {
    //            convertView = _layoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
    //        }

    //        var item = Items[position];

    //        //var nameTextView = convertView.FindViewById<TextView>(Resource.Id.ROwItemName);
    //        //nameTextView.Text = item.Name;

    //        return convertView;
    //    }
    //}
}