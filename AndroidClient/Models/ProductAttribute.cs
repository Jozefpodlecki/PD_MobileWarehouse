using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class ProductAttribute : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public int ProductId { get; set; }

        [JsonProperty]
        public int AttributeId { get; set; }

        [JsonProperty]
        public Attribute Attribute { get; set; }

        [JsonProperty]
        public string Value { get; set; }

        public ProductAttribute()
        {

        }

        public ProductAttribute(Parcel parcel)
        {
            ProductId = parcel.ReadInt();
            AttributeId = parcel.ReadInt();
            Attribute = (Attribute)parcel.ReadParcelable(new Attribute().Class.ClassLoader);
            Value = parcel.ReadString();
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(ProductId);
            dest.WriteInt(AttributeId);
            dest.WriteParcelable(Attribute, ParcelableWriteFlags.None);
            dest.WriteString(Value);
        }

        private static readonly GenericParcelableCreator<ProductAttribute> _creator = new GenericParcelableCreator<ProductAttribute>((parcel) => new ProductAttribute(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<ProductAttribute> GetCreator()
        {
            return _creator;
        }
    }
}