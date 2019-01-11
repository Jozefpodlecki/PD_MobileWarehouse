using System;

namespace Common.DTO
{
    public class GoodsDispatchedNote : BaseEntity
    {
        public DateTime IssueDate { get; set; }
        public DateTime DispatchDate { get; set; }
        public string DocumentId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
