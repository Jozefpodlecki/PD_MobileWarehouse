using Android.OS;
using Android.Runtime;
using Client.Helpers;
using Java.Interop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Models
{
    public class GoodsDispatchedNote : Java.Lang.Object, IParcelable
    {
        [JsonProperty]
        public int InvoiceId { get; set; }

        [JsonProperty]
        public Invoice Invoice { get; set; }

        [JsonProperty]
        public DateTime IssueDate { get; set; }

        [JsonProperty]
        public DateTime DispatchDate { get; set; }

        [JsonProperty]
        public int AuthorId { get; set; }

        [JsonProperty]
        public User Author { get; set; }

        [JsonProperty]
        public string Remarks { get; set; }

        [JsonProperty]
        public string DocumentId { get; set; }

        [JsonProperty]
        public List<NoteEntry> NoteEntry { get; set; }

        public GoodsDispatchedNote()
        {

        }

        public GoodsDispatchedNote(Parcel parcel)
        {
            IssueDate = new DateTime(parcel.ReadLong());
            DispatchDate = new DateTime(parcel.ReadLong());
            AuthorId = parcel.ReadInt();
            Author = (User)parcel.ReadParcelable(new User().Class.ClassLoader);
            Remarks = parcel.ReadString();
            DocumentId = parcel.ReadString();
            InvoiceId = parcel.ReadInt();
            Invoice = (Invoice)parcel.ReadParcelable(new Invoice().Class.ClassLoader);
            NoteEntry = parcel.ReadParcelableArray(new NoteEntry().Class.ClassLoader).Cast<NoteEntry>().ToList();
        }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteLong(IssueDate.Ticks);
            dest.WriteLong(DispatchDate.Ticks);
            dest.WriteInt(AuthorId);
            dest.WriteParcelable(Author, ParcelableWriteFlags.None);
            dest.WriteString(Remarks);
            dest.WriteString(DocumentId);
            dest.WriteInt(InvoiceId);
            dest.WriteParcelable(Invoice, ParcelableWriteFlags.None);
            dest.WriteParcelableArray(NoteEntry.ToArray(), ParcelableWriteFlags.None);
        }

        private static readonly GenericParcelableCreator<GoodsDispatchedNote> _creator = new GenericParcelableCreator<GoodsDispatchedNote>((parcel) => new GoodsDispatchedNote(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<GoodsDispatchedNote> GetCreator()
        {
            return _creator;
        }
    }
}