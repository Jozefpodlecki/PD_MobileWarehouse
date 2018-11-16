using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public enum AttributeType : byte
    {
        Text,
        Int,
        CheckBox,
        DateTime,
        Time
    }

    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AttributeType Type { get; set; }

        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        public bool IsDeleted { get; set; }

        public int Order { get; set; }

        public DateTime LastModification { get; set; }
        public virtual User LastModificationBy { get; set; }
        public int LastModificationById { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User CreatedBy { get; set; }
        public int CreatedById { get; set; }
    }
}
