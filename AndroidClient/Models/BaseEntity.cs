using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;
using System;

namespace Client.Models
{
    public class BaseEntity : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public DateTime CreatedAt { get; set; }

        [JsonProperty]
        public User CreatedBy { get; set; }

        [JsonProperty]
        public DateTime LastModifiedAt { get; set; }

        [JsonProperty]
        public User LastModifiedBy { get; set; }

        public BaseEntity()
        {

        }

        public BaseEntity(Parcel parcel)
        {
            ReadFromParcel(parcel);
        }

        public void ReadFromParcel(Parcel parcel)
        {
            CreatedAt = new DateTime(parcel.ReadLong());
            CreatedBy = (User)parcel.ReadParcelable(new User().Class.ClassLoader);
            LastModifiedAt = new DateTime(parcel.ReadLong());
            LastModifiedBy = (User)parcel.ReadParcelable(new User().Class.ClassLoader);
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteLong(CreatedAt.Ticks);
            dest.WriteParcelable(CreatedBy, ParcelableWriteFlags.None);
            dest.WriteLong(LastModifiedAt.Ticks);
            dest.WriteParcelable(LastModifiedBy, ParcelableWriteFlags.None);
        }

        private static readonly GenericParcelableCreator<BaseEntity> _creator = new GenericParcelableCreator<BaseEntity>((parcel) => new BaseEntity(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<BaseEntity> GetCreator()
        {
            return _creator;
        }
    }
}