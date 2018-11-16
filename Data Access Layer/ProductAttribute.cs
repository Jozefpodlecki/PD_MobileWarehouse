using System;

namespace Data_Access_Layer
{
    public class ProductAttribute
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public virtual Attribute Attribute { get; set; }
        public int AttributeId { get; set; }

        public string Value { get; set; }
    }
}
