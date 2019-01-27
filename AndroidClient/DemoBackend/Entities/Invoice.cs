using Common;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Invoice : BaseEntity, IName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Counterparty))]
        public int CounterpartyId { get; set; }

        [OneToOne]
        public Counterparty Counterparty { get; set; }

        [NotNull]
        public DateTime IssueDate { get; set; }

        [NotNull]
        public DateTime CompletionDate { get; set; }

        [NotNull, Unique, MaxLength(50)]
        public string DocumentId { get; set; }

        [OneToMany]
        public List<Entry> Products { get; set; }

        [NotNull]
        public decimal Total { get; set; }

        [NotNull]
        public decimal VAT { get; set; }

        [ForeignKey(typeof(City))]
        public int CityId { get; set; }

        [OneToOne]
        public City City { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public InvoiceType InvoiceType { get; set; }

        public bool CanEdit { get; set; }

        [OneToOne]
        public GoodsDispatchedNote GoodsDispatchedNote { get; set; }

        [OneToOne]
        public GoodsReceivedNote GoodsReceivedNote { get; set; }

        public string Name => DocumentId;
    }
}
