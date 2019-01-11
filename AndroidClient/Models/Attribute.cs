using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class Attribute : BaseEntity, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        public Attribute()
        {

        }

        public Attribute(Parcel parcel)
        {
            Id = parcel.ReadInt();
            Name = parcel.ReadString();
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(Id);
            dest.WriteString(Name);
        }

        private static readonly GenericParcelableCreator<Attribute> _creator = new GenericParcelableCreator<Attribute>((parcel) => new Attribute(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Attribute> GetCreator()
        {
            return _creator;
        }

        public override string ToString() => Name;
    }
}