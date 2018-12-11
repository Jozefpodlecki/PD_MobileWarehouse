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
using Newtonsoft.Json;

namespace Client.Models
{
    public class Product : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Image { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public DateTime LastModification { get; set; }

        public Product()
        {

        }

        public Product(Parcel parcel)
        {
            
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            
        }

        private static readonly GenericParcelableCreator<Product> _creator = new GenericParcelableCreator<Product>((parcel) => new Product(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Product> GetCreator()
        {
            return _creator;
        }
    }
}