using Common;
using System.Collections.Generic;

namespace Data_Access_Layer
{

    public class Attribute : BaseEntity, IName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        public int Order { get; set; }

    }
}
