using System;
using Android.OS;
using Android.Runtime;
using AndroidClient.Helpers;
using Java.Interop;

namespace Client.Models
{
    public class GoodsReceivedNote : Java.Lang.Object, IParcelable
    {
        public GoodsReceivedNote()
        {

        }

        public GoodsReceivedNote(Parcel parcel)
        {

        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            
        }

        private static readonly GenericParcelableCreator<GoodsReceivedNote> _creator = new GenericParcelableCreator<GoodsReceivedNote>((parcel) => new GoodsReceivedNote(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<GoodsReceivedNote> GetCreator()
        {
            return _creator;
        }
    }
}