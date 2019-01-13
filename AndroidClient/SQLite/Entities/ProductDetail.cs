using System;

namespace Data_Access_Layer
{
    public class ProductDetail : IBaseEntity
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual Location Location { get; set; }
        public int LocationId { get; set; }

        public int Count { get; set; }

        public DateTime? CreatedAt { get; set; }
        public User CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public User LastModifiedBy { get; set; }
        public int? LastModifiedById { get; set; }
    }
}
