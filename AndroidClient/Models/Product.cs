using System;
using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;

namespace Client.Models
{
    public class Product : BaseEntity, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Avatar { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public DateTime LastModification { get; set; }

        [JsonProperty]
        public string Barcode { get; set; }

        [JsonProperty]
        public List<ProductAttribute> ProductAttributes { get; set; }

        [JsonProperty]
        public List<ProductDetail> ProductDetails { get; set; }

        public Product()
        {

        }

        public Product(Parcel parcel)
        {
            Id = parcel.ReadInt();
            Avatar = parcel.ReadString();
            Name = parcel.ReadString();
            LastModification = new DateTime(parcel.ReadLong());
            Barcode = parcel.ReadString();
            ProductAttributes = parcel.ReadParcelableArray(new ProductAttribute().Class.ClassLoader).Cast<ProductAttribute>().ToList();
            ProductDetails = parcel.ReadParcelableArray(new ProductDetail().Class.ClassLoader).Cast<ProductDetail>().ToList();
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(Id);
            dest.WriteString(Avatar);
            dest.WriteString(Name);
            dest.WriteLong(LastModification.Ticks);
            dest.WriteString(Barcode);
            dest.WriteParcelableArray(ProductAttributes.ToArray(), ParcelableWriteFlags.None);
            dest.WriteParcelableArray(ProductDetails.ToArray(), ParcelableWriteFlags.None);
        }

        private static readonly GenericParcelableCreator<Product> _creator = new GenericParcelableCreator<Product>((parcel) => new Product(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Product> GetCreator()
        {
            return _creator;
        }
    }
}