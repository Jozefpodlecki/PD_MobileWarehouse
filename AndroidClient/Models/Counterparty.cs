using Android.OS;
using Android.Runtime;
using AndroidClient.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class Counterparty : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string PostalCode { get; set; }

        [JsonProperty]
        public string Street { get; set; }

        [JsonProperty]
        public City City { get; set; }

        [JsonProperty]
        public string PhoneNumber { get; set; }

        [JsonProperty]
        public string NIP { get; set; }

        public Counterparty()
        {

        }

        public Counterparty(Parcel parcel)
        {
            Id = parcel.ReadInt();
            Name = parcel.ReadString();
            PostalCode = parcel.ReadString();
            Street = parcel.ReadString();
            //dest.WriteParcelable();
            PhoneNumber = parcel.ReadString();
            NIP = parcel.ReadString();
        }

        public int DescribeContents() => 0;

        public override string ToString() => Name;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(Id);
            dest.WriteString(Name);
            dest.WriteString(PostalCode);
            dest.WriteString(Street);
            //dest.WriteParcelable();
            dest.WriteString(PhoneNumber);
            dest.WriteString(NIP);
        }

        private static readonly GenericParcelableCreator<Counterparty> _creator = new GenericParcelableCreator<Counterparty>((parcel) => new Counterparty(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Counterparty> GetCreator()
        {
            return _creator;
        }
    }
}