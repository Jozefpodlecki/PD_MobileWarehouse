using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class Location : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        public Location()
        {

        }

        public Location(Parcel parcel)
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

        private static readonly GenericParcelableCreator<Location> _creator = new GenericParcelableCreator<Location>((parcel) => new Location(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Location> GetCreator()
        {
            return _creator;
        }

        public override string ToString() => Name;
    }
}