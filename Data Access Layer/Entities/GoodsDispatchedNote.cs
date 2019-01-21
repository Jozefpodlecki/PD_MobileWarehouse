using Common;
using System;

namespace Data_Access_Layer
{
    public class GoodsDispatchedNote : IBaseEntity, IName
    {
        public DateTime IssueDate { get; set; }

        public string Remarks { get; set; }

        public string DocumentId { get; set; }

        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public string Name => DocumentId;

        public DateTime DispatchDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public virtual User CreatedBy { get; set; }
        public int? CreatedById { get; set; }
    
        public DateTime LastModifiedAt { get; set; }
        public virtual User LastModifiedBy { get; set; }
        public int? LastModifiedById { get; set; }
    }
}
