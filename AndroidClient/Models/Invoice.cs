using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Common;
using Java.Interop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Models
{
    public class Invoice : BaseEntity, IParcelable
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public City City { get; set; }

        [JsonProperty]
        public Counterparty Counterparty { get; set; }

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
        public decimal VAT { get; set; }

        [JsonProperty]
        public decimal Total { get; set; }

        [JsonProperty]
        public List<Entry> Products { get; set; }

        [JsonProperty]
        public GoodsDispatchedNote GoodsDispatchedNote { get; set; }

        [JsonProperty]
        public GoodsReceivedNote GoodsReceivedNote { get; set; }

        public Invoice()
        {
        }

        public Invoice(Parcel parcel)
        {
            Id = parcel.ReadInt();
            City = (City)parcel.ReadParcelable(new City().Class.ClassLoader);
            Counterparty = (Counterparty)parcel.ReadParcelable(new Counterparty().Class.ClassLoader);
            IssueDate = new DateTime(parcel.ReadLong());
            CompletionDate = new DateTime(parcel.ReadLong());
            DocumentId = parcel.ReadString();
            Author = parcel.ReadString();
            InvoiceType = (InvoiceType)parcel.ReadByte();
            PaymentMethod = (PaymentMethod)parcel.ReadByte();
            Total = decimal.Parse(parcel.ReadString());
            VAT = decimal.Parse(parcel.ReadString());
            Products = parcel.ReadParcelableArray(new Entry().Class.ClassLoader).Cast<Entry>().ToList();
            GoodsDispatchedNote = (GoodsDispatchedNote)parcel.ReadParcelable(new GoodsDispatchedNote().Class.ClassLoader);
            GoodsReceivedNote = (GoodsReceivedNote)parcel.ReadParcelable(new GoodsReceivedNote().Class.ClassLoader);
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(Id);
            dest.WriteParcelable(City, ParcelableWriteFlags.None);
            dest.WriteParcelable(Counterparty, ParcelableWriteFlags.None);
            dest.WriteLong(IssueDate.Ticks);
            dest.WriteLong(CompletionDate.Ticks);
            dest.WriteString(DocumentId);
            dest.WriteString(Author);
            dest.WriteByte((sbyte)InvoiceType);
            dest.WriteByte((sbyte)PaymentMethod);
            dest.WriteString(Total.ToString());
            dest.WriteString(VAT.ToString());
            dest.WriteParcelableArray(Products.ToArray(), ParcelableWriteFlags.None);
            dest.WriteParcelable(GoodsDispatchedNote, ParcelableWriteFlags.None);
            dest.WriteParcelable(GoodsReceivedNote, ParcelableWriteFlags.None);
        }

        private static readonly GenericParcelableCreator<Invoice> _creator = new GenericParcelableCreator<Invoice>((parcel) => new Invoice(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Invoice> GetCreator()
        {
            return _creator;
        }

        public override string ToString() => DocumentId;
    }
}