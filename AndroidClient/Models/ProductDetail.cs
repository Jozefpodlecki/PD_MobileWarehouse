using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class ProductDetail : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public Location Location { get; set; }

        [JsonProperty]
        public int Count { get; set; }

        public ProductDetail()
        {

        }

        public ProductDetail(Parcel parcel)
        {

        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {

        }

        private static readonly GenericParcelableCreator<ProductDetail> _creator = new GenericParcelableCreator<ProductDetail>((parcel) => new ProductDetail(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<ProductDetail> GetCreator()
        {
            return _creator;
        }
    }
}