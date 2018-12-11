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
using Newtonsoft.Json;

namespace Client.Models
{
    public class Role : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public IEnumerable<Claim> Claims { get; set; }

        public Role()
        {

        }

        public Role(Parcel parcel)
        {
            Id = parcel.ReadInt();
            Name = parcel.ReadString();
            var classLoader = Java.Lang.Class.FromType(typeof(Role)).ClassLoader;
            Claims = parcel.ReadParcelableArray(classLoader).Cast<Claim>();
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(Id);
            dest.WriteString(Name);
            dest.WriteParcelableArray(Claims.ToArray(),ParcelableWriteFlags.None);
        }
    }
}