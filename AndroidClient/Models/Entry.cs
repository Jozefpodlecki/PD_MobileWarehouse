using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class Entry : BaseEntity, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int Count { get; set; }

        [JsonProperty]
        public decimal VAT { get; set; }

        [JsonProperty]
        public decimal Price { get; set; }

        public Entry()
        {

        }

        public Entry(Parcel parcel)
        {
            Id = parcel.ReadInt();
            Name = parcel.ReadString();
            Count = parcel.ReadInt();
            VAT = decimal.Parse(parcel.ReadString());
            Price = decimal.Parse(parcel.ReadString());
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(Id);
            dest.WriteString(Name);
            dest.WriteInt(Count);
            dest.WriteString(VAT.ToString());
            dest.WriteString(Price.ToString());
        }

        private static readonly GenericParcelableCreator<Entry> _creator = new GenericParcelableCreator<Entry>((parcel) => new Entry(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Entry> GetCreator()
        {
            return _creator;
        }
    }
}