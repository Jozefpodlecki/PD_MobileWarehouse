using Common;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Invoice.ViewModel
{
    public class AddInvoice
    {
        public Common.DTO.Counterparty Counterparty { get; set; }

        public string DocumentId { get; set; }

        public ICollection<Entry> Products { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public Common.DTO.City City { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public InvoiceType InvoiceType { get; set; }

        public bool CanEdit { get; set; }
    }
}
