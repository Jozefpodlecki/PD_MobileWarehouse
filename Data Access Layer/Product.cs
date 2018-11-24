using Common;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Product : IName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public decimal VAT { get; set; }

        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
        public virtual ICollection<ProductDetails> ProductDetails { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime LastModification { get; set; }

        public virtual User LastModificationBy { get; set; }
        public int LastModificationById { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User CreatedBy { get; set; }
        public int CreatedById { get; set; }
    }
}
