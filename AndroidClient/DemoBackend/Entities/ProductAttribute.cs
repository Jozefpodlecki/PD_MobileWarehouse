using SQLiteNetExtensions.Attributes;
using System;

namespace Data_Access_Layer
{
    public class ProductAttribute : BaseEntity
    {
        [OneToOne]
        public virtual Product Product { get; set; }

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }

        [OneToOne]
        public Attribute Attribute { get; set; }

        [ForeignKey(typeof(Attribute))]
        public int AttributeId { get; set; }

        public string Value { get; set; }
    }
}
