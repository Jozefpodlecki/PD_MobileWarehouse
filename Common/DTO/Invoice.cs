using System;
using System.Collections.Generic;

namespace Common.DTO
{
    public class Invoice
    {
        public DateTime IssueDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string DocumentId { get; set; }
        public string Author { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<Entry> Products { get; set; }
    }
}
