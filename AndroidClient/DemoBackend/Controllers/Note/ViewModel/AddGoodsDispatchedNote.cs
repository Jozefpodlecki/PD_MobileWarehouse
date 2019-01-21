using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class AddGoodsDispatchedNote
    {
        public string DocumentId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DispatchDate { get; set; }

        public int InvoiceId { get; set; }

        public List<NoteEntry> NoteEntry { get; set; }
    }
}
