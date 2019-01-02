using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class AddGoodsReceivedNote
    {
        [Required]
        public string DocumentId { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime ReceiveDate { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public List<NoteEntry> NoteEntry { get; set; }
    }
}
