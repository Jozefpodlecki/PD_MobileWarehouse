using Common;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Invoice.ViewModel
{
    public class AddInvoice
    {
        [Required]
        public Common.DTO.Counterparty Counterparty { get; set; }

        [Required]
        public string DocumentId { get; set; }

        [Required]
        public ICollection<Entry> Products { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        [Required]
        public Common.DTO.City City { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public InvoiceType InvoiceType { get; set; }

        public bool CanEdit { get; set; }
    }
}
