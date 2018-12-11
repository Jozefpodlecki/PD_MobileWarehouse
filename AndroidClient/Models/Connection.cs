using System;
using Android.OS;
using Android.Runtime;

namespace AndroidClient.Models
{
    public class Connection : Java.Lang.Object, IParcelable
    {
        public string ServerName { get; set; }
        public string ServerType { get; set; }
        public int Port { get; set; }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            throw new NotImplementedException();
        }
    }
}