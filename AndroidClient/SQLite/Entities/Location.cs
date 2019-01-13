using Common;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Location : BaseEntity, IName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
