using Android.OS;
using Android.Views;

namespace Client.Fragments.Details
{
    public class GoodsReceivedNote : BaseFragment
    {
        public Models.GoodsReceivedNote Entity { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.GoodsReceivedNoteDetails, container, false);

            Entity = (Models.GoodsReceivedNote)Arguments.GetParcelable(Constants.Entity);

            return view;
        }
    }
}