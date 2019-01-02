using Common;
using System;

namespace Data_Access_Layer
{
    public class Note : IName
    {
        public DateTime IssueDate { get; set; }

        public string Remarks { get; set; }

        public string DocumentId { get; set; }

        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public string Name => DocumentId;
    }
}
