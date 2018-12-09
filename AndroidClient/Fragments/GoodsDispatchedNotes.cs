using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Client.Fragments
{
    public class GoodsDispatchedNotes : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddGoodDispatchedNoteFloatActionButton { get; set; }

        public void AfterTextChanged(IEditable s)
        {
            throw new NotImplementedException();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.GoodsDispatchedNotes, container, false);

            AddGoodDispatchedNoteFloatActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddGoodDispatchedNoteFloatActionButton);
            AddGoodDispatchedNoteFloatActionButton.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View view)
        {
            NavigationManager.GoToAddGoodsDispatchedNote();
        }
    }
}