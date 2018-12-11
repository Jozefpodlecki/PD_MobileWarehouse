using Android.OS;
using Android.Runtime;
using AndroidClient.Helpers;
using Common;
using Java.Interop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Client.Models
{
    public class Invoice : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public DateTime IssueDate { get; set; }

        [JsonProperty]
        public DateTime CompletionDate { get; set; }

        [JsonProperty]
        public string DocumentId { get; set; }

        [JsonProperty]
        public string Author { get; set; }

        [JsonProperty]
        public InvoiceType InvoiceType { get; set; }

        [JsonProperty]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonProperty]
        public List<Entry> Products { get; set; }

        public Invoice()
        {

        }

        public Invoice(Parcel parcel)
        {
            IssueDate = new DateTime(parcel.ReadLong());
            CompletionDate = new DateTime(parcel.ReadLong());
            DocumentId = parcel.ReadString();
            Author = parcel.ReadString();
            InvoiceType = (InvoiceType)parcel.ReadByte();
            PaymentMethod = (PaymentMethod)parcel.ReadByte();
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteLong(IssueDate.Ticks);
            dest.WriteLong(CompletionDate.Ticks);
            dest.WriteString(DocumentId);
            dest.WriteString(Author);
            dest.WriteByte((sbyte)InvoiceType);
            dest.WriteByte((sbyte)PaymentMethod);
        }

        private static readonly GenericParcelableCreator<Invoice> _creator = new GenericParcelableCreator<Invoice>((parcel) => new Invoice(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Invoice> GetCreator()
        {
            return _creator;
        }
    }
}