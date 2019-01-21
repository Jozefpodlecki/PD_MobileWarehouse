using System;

namespace Common.DTO
{
    public class GoodsReceivedNote : BaseEntity
    {
        public DateTime IssueDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string DocumentId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
