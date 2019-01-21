using SQLiteNetExtensions.Attributes;
using System;

namespace Data_Access_Layer
{
    public class BaseEntity : IBaseEntity
    {
        public DateTime CreatedAt { get; set; }

        [OneToOne]
        public virtual User CreatedBy { get; set; }

        [ForeignKey(typeof(User))]
        public int? CreatedById { get; set; }

        public DateTime LastModifiedAt { get; set; }

        [OneToOne]
        public virtual User LastModifiedBy { get; set; }

        [ForeignKey(typeof(User))]
        public int? LastModifiedById { get; set; }
    }
}
