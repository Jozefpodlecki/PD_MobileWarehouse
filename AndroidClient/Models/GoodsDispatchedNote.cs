using Android.OS;
using Android.Runtime;
using AndroidClient.Helpers;
using Java.Interop;

namespace Client.Models
{
    public class GoodsDispatchedNote : Java.Lang.Object, IParcelable
    {
        public GoodsDispatchedNote()
        {

        }

        public GoodsDispatchedNote(Parcel parcel)
        {
            
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            
        }

        private static readonly GenericParcelableCreator<GoodsDispatchedNote> _creator = new GenericParcelableCreator<GoodsDispatchedNote>((parcel) => new GoodsDispatchedNote(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<GoodsDispatchedNote> GetCreator()
        {
            return _creator;
        }
    }
}