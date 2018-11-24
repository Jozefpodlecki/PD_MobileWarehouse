using System;

namespace Common.DTO
{
    public class GoodsDispatchedNote
    {
        public DateTime IssueDate { get; set; }
        public DateTime DispatchDate { get; set; }
        public Invoice Invoice { get; set; }
    }
}
