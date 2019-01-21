using Common;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Data_Access_Layer
{
    public class GoodsDispatchedNote : BaseEntity, IName
    {
        [NotNull]
        public DateTime IssueDate { get; set; }

        public string Remarks { get; set; }

        [NotNull, Unique, MaxLength(50)]
        public string DocumentId { get; set; }

        [ForeignKey(typeof(Invoice))]
        public int InvoiceId { get; set; }

        [OneToOne]
        public virtual Invoice Invoice { get; set; }

        public string Name => DocumentId;

        [NotNull]
        public DateTime DispatchDate { get; set; }
    }
}
