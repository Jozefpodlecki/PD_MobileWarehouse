using System;

namespace Common.DTO
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public User CreatedBy { get; set; }

        public DateTime LastModifiedAt { get; set; }
        public User LastModifiedBy { get; set; }
    }
}
