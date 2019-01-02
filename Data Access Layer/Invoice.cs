using Common;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Invoice
    {
        public int Id { get; set; }

        public int CounterpartyId { get; set; }

        public virtual Counterparty Counterparty { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public string DocumentId { get; set; }

        public virtual ICollection<Entry> Products { get; set; }

        public decimal Total { get; set; }

        public decimal VAT { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public InvoiceType InvoiceType { get; set; }

        public bool CanEdit { get; set; }

        public virtual GoodsDispatchedNote GoodsDispatchedNote { get; set; }

        public virtual GoodsReceivedNote GoodsReceivedNote { get; set; }
    }
}
