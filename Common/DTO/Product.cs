using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public DateTime LastModification { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
    }
}
