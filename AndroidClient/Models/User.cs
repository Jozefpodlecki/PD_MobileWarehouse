using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class User : BaseEntity, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string FirstName { get; set; }

        [JsonProperty]
        public string LastName { get; set; }

        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public string Password { get; set; }

        [JsonProperty]
        public string Token { get; set; }

        [JsonProperty]
        public Role Role { get; set; }

        [JsonProperty]
        public string Email { get; set; }

        [JsonProperty]
        public string Avatar { get; set; }

        [JsonProperty]
        public List<Claim> Claims { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public User()
        {

        }

        private User(Parcel parcel)
        {
            Id = parcel.ReadInt();
            FirstName = parcel.ReadString();
            LastName = parcel.ReadString();
            Username = parcel.ReadString();
            Password = parcel.ReadString();
            Token = parcel.ReadString();
            Email = parcel.ReadString();
            Avatar = parcel.ReadString();
            Role = (Role)parcel.ReadParcelable(new Role().Class.ClassLoader);
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            base.WriteToParcel(dest, flags);
            dest.WriteInt(Id);
            dest.WriteString(FirstName);
            dest.WriteString(LastName);
            dest.WriteString(Username);
            dest.WriteString(Password);
            dest.WriteString(Token);
            dest.WriteString(Email);
            dest.WriteString(Avatar);
            dest.WriteParcelable(Role, ParcelableWriteFlags.None);
        }

        public int DescribeContents() => 0;

        private static readonly GenericParcelableCreator<User> _creator = new GenericParcelableCreator<User>((parcel) => new User(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<User> GetCreator()
        {
            return _creator;
        }

        public override string ToString() => Username;
    }    
}