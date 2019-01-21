using System;

namespace Data_Access_Layer
{
    public class BaseEntity : IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public virtual User CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        public DateTime LastModifiedAt { get; set; }
        public virtual User LastModifiedBy { get; set; }
        public int? LastModifiedById { get; set; }
    }
}
