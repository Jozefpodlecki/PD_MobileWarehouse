using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Java.Lang;

namespace Client.Adapters
{
    public class GoodsReceivedNotesAdapter : RecyclerView.Adapter
    {
        public override int ItemCount => throw new System.NotImplementedException();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position, IList<Object> payloads)
        {
            base.OnBindViewHolder(holder, position, payloads);
        }

        public override long GetItemId(int position)
        {
            return base.GetItemId(position);
        }

        public override int GetItemViewType(int position)
        {
            return base.GetItemViewType(position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new System.NotImplementedException();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new System.NotImplementedException();
        }

        public override void OnViewAttachedToWindow(Object holder)
        {
            base.OnViewAttachedToWindow(holder);
        }

        public override void OnViewDetachedFromWindow(Object holder)
        {
            base.OnViewDetachedFromWindow(holder);
        }

        public override void OnViewRecycled(Object holder)
        {
            base.OnViewRecycled(holder);
        }
    }
}