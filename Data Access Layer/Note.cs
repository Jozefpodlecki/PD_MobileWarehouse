using Common;
using System;

namespace Data_Access_Layer
{
    public class Note : IName
    {
        public int Id { get; set; }

        public DateTime IssueDate { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string Remarks { get; set; }

        public string DocumentId { get; set; }

        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public string Name => DocumentId;
    }
}
