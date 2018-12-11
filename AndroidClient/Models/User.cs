using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using AndroidClient.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class User : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Surname { get; set; }

        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public string Password { get; set; }

        [JsonProperty]
        public string Token { get; set; }

        [JsonProperty]
        public bool CanRenew { get; set; }

        [JsonProperty]
        public string Role { get; set; }

        [JsonProperty]
        public string Email { get; set; }

        [JsonProperty]
        public List<Claim> Claims { get; set; }

        public User()
        {

        }

        private User(Parcel parcel)
        {
            Name = parcel.ReadString();
            Surname = parcel.ReadString();
            Username = parcel.ReadString();
            Password = parcel.ReadString();
            Token = parcel.ReadString();
            //dest.WriteByte(CanRenew ? (sbyte)1 : (sbyte)0);
            Role = parcel.ReadString();
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteString(Name);
            dest.WriteString(Surname);
            dest.WriteString(Username);
            dest.WriteString(Password);
            dest.WriteString(Token);
            dest.WriteByte(CanRenew ? (sbyte)1 : (sbyte)0);
            dest.WriteString(Role);
        }

        public int DescribeContents() => 0;

        private static readonly GenericParcelableCreator<User> _creator = new GenericParcelableCreator<User>((parcel) => new User(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<User> GetCreator()
        {
            return _creator;
        }
    }    
}