using Common;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Product : BaseEntity, IName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public decimal VAT { get; set; }

        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }

        public string Barcode { get; set; }
    }
}
