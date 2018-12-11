using Android.OS;
using Android.Runtime;
using AndroidClient.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class Claim : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public string Type { get; set; }

        [JsonProperty]
        public string Value { get; set; }

        [JsonProperty]
        public bool Checked { get; set; }

        public Claim()
        {
            
        }

        public Claim(Parcel parcel)
        {
            Type = parcel.ReadString();
            Value = parcel.ReadString();
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteString(Type);
            dest.WriteString(Value);
        }

        private static readonly GenericParcelableCreator<Claim> _creator = new GenericParcelableCreator<Claim>((parcel) => new Claim(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Claim> GetCreator()
        {
            return _creator;
        }
    }
}