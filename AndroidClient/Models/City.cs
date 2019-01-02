using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class City : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        public City()
        {

        }

        public City(Parcel parcel)
        {
            Id = parcel.ReadInt();
            Name = parcel.ReadString();
        }

        public int DescribeContents() => 0;

        public override string ToString() => Name;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(Id);
            dest.WriteString(Name);
        }

        private static readonly GenericParcelableCreator<City> _creator = new GenericParcelableCreator<City>((parcel) => new City(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<City> GetCreator()
        {
            return _creator;
        }
    }
}