using Common;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class Invoice
    {
        public int Id { get; set; }

        public string DocumentId { get; set; }

        public ICollection<Entry> Products { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public City City { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public bool CanEdit { get; set; }
    }
}
