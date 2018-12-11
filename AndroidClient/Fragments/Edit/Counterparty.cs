using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Edit
{
    public class Counterparty : BaseFragment
    {
        public Models.Counterparty Entity { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CounterpartiesEdit, container, false);

            Entity = (Models.Counterparty)this.Arguments.GetParcelable("data");

            var test = Entity.Id;
            var tsss = Entity.Name;

            return view;
        }
    }
}