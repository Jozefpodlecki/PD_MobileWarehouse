using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidClient.Helpers;
using Java.Interop;

namespace AndroidClient.Models
{
    public class User : Java.Lang.Object, IParcelable
    {
        private static readonly GenericParcelableCreator<User> _creator
        = new GenericParcelableCreator<User>((parcel) => new User(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<User> GetCreator()
        {
            return _creator;
        }

        public User()
        {

        }

        private User(Parcel parcel)
        {

        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public bool CanRenew { get; set; }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteString(Name);
            dest.WriteString(Surname);
            dest.WriteString(Username);
            dest.WriteString(Password);
            dest.WriteString(Token);
            dest.WriteByte(CanRenew ? (sbyte)1 : (sbyte)0);
        }

        public int DescribeContents()
        {
            return 0;
        }
    }    
}