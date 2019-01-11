using Android.OS;
using Android.Views;

namespace Client.Fragments.Details
{
    public class GoodsReceivedNote : BaseDetailsFragment<Models.GoodsReceivedNote>
    {
        public GoodsReceivedNote() : base(Resource.Layout.GoodsReceivedNoteDetails)
        {
        }

        public override void OnBindElements(View view)
        {
            throw new System.NotImplementedException();
        }
    }
}