using System;
using System.Collections.Generic;

namespace Common.DTO
{
    public class Invoice
    {
        public int Id { get; set; }
        public City City { get; set; }
        public Counterparty Counterparty { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string DocumentId { get; set; }
        public string Author { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<Entry> Products { get; set; }
        public decimal VAT { get; set; }
        public decimal Total { get; set; }
    }
}
